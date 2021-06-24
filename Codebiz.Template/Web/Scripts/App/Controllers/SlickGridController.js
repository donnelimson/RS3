var app = angular.module('MetronicApp');
app.requires.push.apply(app.requires, ['INVNTRY.DATASERVICE']);
app.controller('SlickGridController', ['$scope', '$q', '$uibModal', 'CommonService', 'ItemMasterService', 'WarehouseService',
    function ($scope, $q, $uibModal, CommonService, ItemMasterService, WarehouseService) {
        $scope.grid;
        $scope.dataGrid;
        $scope.columnsGrid;
        $scope.optionsGrid;

        //#region Grid

        $scope.loadGrid = function () {
            $scope.grid = new Slick.Grid("#gridForm", $scope.dataGrid, $scope.columnsGrid, $scope.optionsGrid);
            $scope.grid.setSelectionModel(new Slick.CellSelectionModel());
            var oldVal = [];
            $scope.$watch('grid', function (newVal, oldVal) {
                newVal.onCellChange.subscribe(function (e, args) {
                    $scope.f.$pristine = false;
                });

            });

        }

        $scope.addGridRow = function () {
            var hasEmptyRow = false;
            for (var i = 0; i < $scope.dataGrid.length; i++) {
                var data = $scope.dataGrid[i];
                hasEmptyRow = data.ItemNo == undefined;

                if (hasEmptyRow)
                    break;
            }

            if (!hasEmptyRow) {
                $scope.dataGrid.push({ LineID: $scope.dataGrid.length + 1 })
                $scope.grid.invalidateRows([$scope.dataGrid.length - 1]);
                $scope.grid.updateRowCount();
                $scope.f.$pristine = false;
            }

            renderGrid();
        }

        $scope.validateRow = function () {
            var result = true;
            for (var rowIdx = 0; rowIdx < $scope.dataGrid.length - 1; rowIdx++) {
                var item = $scope.grid.getDataItem(rowIdx);
                var columns = $scope.grid.getColumns();

                $.each(columns, function (colIdx, column) {
                    // iterate through editable cells
                    if (column.editor && column.validator) {
                        var validationResults = column.validator(item[column.field]);
                        if (!validationResults.valid) {
                            result = false;

                            // show editor
                            $scope.grid.gotoCell(rowIdx, colIdx, true);
                            $scope.grid.flashCell(rowIdx, colIdx, 200);

                            // validate (it will fail)
                            $scope.grid.getEditorLock().commitCurrentEdit();

                            // stop iteration
                            return result;
                        }
                    }
                });
            }

            return result;
        }

        function renderGrid() {
            $scope.grid.invalidateAllRows();
            $scope.grid.render();
        }

        //#endregion

        //#region Grid Editor

        $scope.SelectListLookUp = function (args) {
            var $input;
            var defaultValue;
            var scope = this;

            $scope.filter = {
                likType: "",
                searchText: "",
                itemType: ""
            };

            if (args.column.itemType == ITEM_TYPE.Item.Desc) {
                $scope.filter.itemType = ITEM_TYPE.Item.Desc;
            }

            this.init = function () {
                $inputElement = $(`<div class="input-icon right">
                                <i id="selectIcon" name="selectIcon" class="fa fa-search"  style="color:#32D5C2 !important; margin-top:1px !important;"></i>
                                <input id="selectId" name="selectId" type="text" class="editor-text" value="" />
                            </div>`).appendTo(args.container);

                $input = $(`input[name="selectId"]`).select();
                $input.focus().select();
                $input.bind("keydown", scope.handleKeyDown);

                if (args.column.field.trim().toUpperCase().includes("ITEMNO")) {
                    $scope.filter.linkType = 'ITM';
                    if (args.column.requiresExclusion) {
                        $scope.filter.requiresExclusion = true;
                    }
                    if (args.column.type == 'bom') {
                        $scope.filter.type = 'bom';
                    }
                }

                if (args.column.field.trim().toUpperCase().includes("WAREHOUSE")) {
                    $scope.filter.linkType = 'WHS'
                }

                if (args.column.field.trim().toUpperCase().includes("ITEMWHS")) {
                    $scope.filter.linkType = 'ITMWHS'
                }


                $("i[name='selectIcon']").on('click', function (e) {
                    openModalSearch($scope.filter.linkType, "", args, true)
                })

            }
            this.handleKeyDown = function (e) {
                if (e.keyCode == $.ui.keyCode.TAB || e.keyCode == $.ui.keyCode.ENTER) {
                    var field = args.column.field.toUpperCase();
                    var columns = args.grid.getColumns();
                    var value = $input.val();
                    var rowData = $scope.grid.getData()[$scope.grid.getActiveCell().row];

                    if (field.includes("ITEMNO") &&
                        value != null && value.length > 0 &&
                        (value != args.item.ItemNo || args.item.ItemName == "" || args.item.ItemName == undefined)) {

                        rowData.ItemId == null;
                        rowData.ItemName = "";
                        rowData.UnitOfMeasure = null;
                        rowData.UnitOfMeasureId = null;
                        rowData.Quantity = "";

                        ItemMasterService.GetItemByCode(value).then(res => {
                            if (res.StatusCode === '0') {
                                var data = res;
                                args.item.ItemId = data.DataResult.ItemMasterId;
                                args.item.ItemNo = data.DataResult.ItemCode;
                                args.item.ItemName = data.DataResult.ItemName;
                                populateOptionsBasedOnItem(data.DataResult, columns, args);
                                $scope.addGridRow(); // add row
                            }
                            else {
                                openModalSearch($scope.filter.linkType, value, args, e.keyCode == $.ui.keyCode.ENTER);
                            }
                        })
                    }
                    if (field.includes("ITEMWHS") &&
                        value != null && value.length > 0 && (value != args.item.ItemWHS || args.item.ItemWHS != "") && args.item.ItemId != null) {
                        WarehouseService.GetItemWarehouseLookupByWhsName({ Name: value, Id: args.item.ItemId }).then(res => {
                            args.item.WarehouseId = null;
                            args.item.WarehouseCode = "";
                            args.item.AvailableStock = "";
                            if (res.StatusCode === '0') {
                                var data = res;
                                var itemAvailable = data.DataResult.AvailableQty - args.item.Quantity;
                                if (data.DataResult.AvailableQty > 0) {
                                    if (itemAvailable < 0) {
                                        CommonService.warningMessage("Item quantity must not be higher than available stock!");
                                    }
                                    else {
                                        args.item.WarehouseId = data.DataResult.WarehouseId;
                                        args.item.WarehouseCode = data.DataResult.WarehouseCode;
                                        args.item.ItemWHS = data.DataResult.WarehouseName;
                                        args.item.AvailableStock = data.DataResult.AvailableQty;
                                    }
                                }
                                else {
                                    CommonService.warningMessage("No item available");
                                }
                            }
                            else {
                                openModalSearch($scope.filter.linkType, value, args, e.keyCode == $.ui.keyCode.ENTER);
                            }
                            renderGrid();
                        });
                       
                    }
                }
            };
            this.destroy = function () {
                $inputElement.remove();
            }
            this.focus = function () {
                $input.focus();
            }
            this.getValue = function () {
                return $input.val();
            }
            this.setValue = function (val) {
                $input.val(val);
            }
            this.loadValue = function (item) {
                defaultValue = item[args.column.field] || "";
                $input.val(defaultValue);
                $input[0].defaultValue = defaultValue;
                $input.select();
            }
            this.serializeValue = function () {
                return $input.val();
            }
            this.applyValue = function (item, state) {
                item[args.column.field] = state;
            };
            this.isValueChanged = function () {
                return (!($input.val() === "" && defaultValue == null)) && ($input.val() != defaultValue);
            }
            this.validate = function () {
                if (args.column.validator) {
                    var validationResults = args.column.validator($input.val(), $input);
                    if (!validationResults.valid) {
                        return validationResults;
                    }
                }

                return { valid: true, msg: null };
            }
            this.init();
        }

        $scope.SelectList = function (args) {
            var $select;
            var defaultValue;

            this.init = function () {
                $select = $("<div><select id='slickselect'></select></div>");
                $select.appendTo(args.container);
                $select.focus().select();
                $input = $(`select[id="slickselect"]`).select();

                PopulateSelect($input[0], args.item[args.column.name.replace(/\s/g, '') + "DataSource"], true);

                $input.select2({ allowClear: false }).val(args.item[args.column.field]).trigger('change')

                $select.children('span.select2.select2-container.select2-container--bootstrap')
                    .children('span.selection')
                    .children('span.select2-selection.select2-selection--single')
                    .attr('style', `background-color:#fff !important; border: none;!important; color:#555 !important; outline:0; width:100%; height:20px; padding-top:1px ;padding-bottom:1px;padding-left:1px`);
                $input.on('select2:close', function (e) {
                    if ((e.params.originalSelect2Event && e.params.originalSelect2Event.data) || e.params.key === 9) {
                        args.grid.navigateNext();
                    } else {
                        setTimeout(function () {
                            $input.select();
                        }, 100);
                    }
                });
            }
            this.destroy = function () {
                if ($('select').data('select2')) {
                    $('select').select2('close');
                    $select.remove();
                }
            }
            this.focus = function () {
                setTimeout(function () {
                    $input.select();
                }, 100);
            }
            this.loadValue = function (item) {
                defaultValue = item[args.column.field];
                $input.val(defaultValue);
                $input[0].defaultValue = defaultValue;
                $input.trigger("change.select2");
            }
            this.serializeValue = function () {
                return $input.val();
            }
            this.applyValue = function (item, state) {
                PopulateOnSelect(args, item, state);
                $scope.addGridRow(); // Add new row
            };
            this.isValueChanged = function () {
                return (!($input.val() === "" && defaultValue == null)) && ($input.val() != defaultValue);
            };
            this.validate = function () {
                if (args.column.validator) {
                    var validationResults = args.column.validator($input.val(), $input);
                    if (!validationResults.valid) {
                        return validationResults;
                    }
                }

                var data = args.item[args.column.name.replace(/\s/g, '') + "DataSource"].find(r => r.Name == $input.val());
                var row = $scope.grid.getData()[$scope.grid.getActiveCell().row];
                if (args.column.field.toUpperCase().includes("UNITOFMEASURE")) {

                    if (data != null) {
                        args.item["UnitOfMeasureId"] = data.Id;
                        args.item[args.column.field] = data.Description;
                    }
                    else {
                        row.UnitOfMeasureId = $input.val();
                        row.UnitOfMeasure = $input.val();
                    }
                }

                return { valid: true, msg: null };
            }
            this.init();
        }

        $scope.IntegerEditor = function (args) {
            var $input;
            var defaultValue;
            var scope = this;

            this.init = function () {
                $inputElement = $(`<div><input id="inputText" name="inputText" type="number" class='editor-text text-right' value="" maxlength='9' /></div>`);
                $inputElement.appendTo(args.container);

                $input = $(`input[name="inputText"]`).select();
                $input.focus().select();
                $input.bind("keydown", scope.handleKeyDown);
            };
            this.handleKeyDown = function (e) {
                if (e.keyCode == $.ui.keyCode.TAB || e.keyCode == $.ui.keyCode.ENTER) {
                    var field = args.column.field.toUpperCase();
                    var value = $input.val();

                    if (field.includes("QUANTITY") && value != null && value.length > 0) {
                        var unitPrice = args.item.UnitPrice == undefined ? 0 : parseFloat(args.item.UnitPrice);
                        args.item.Total = (unitPrice * value).toFixed(2);
                        args.item.Quantity = parseInt(value);
                    }
                }
            };
            this.destroy = function () {
                $inputElement.remove();
            };
            this.focus = function () {
                $input.focus();
            };
            this.loadValue = function (item) {
                defaultValue = item[args.column.field];
                if (defaultValue != undefined) {
                    $input.val(defaultValue);
                    $input[0].defaultValue = defaultValue;
                }
                $input.select();
            };
            this.serializeValue = function () {
                return parseInt($input.val(), 10) || 0;
            };
            this.applyValue = function (item, state) {
                item[args.column.field] = state;
                if (args.column.type == 'bom') {
                    item.Total = $scope.calculateTotalPrice(item.Total, item.UnitPrice, state);
                }
                $scope.addGridRow(); // Add new row
            };
            this.isValueChanged = function () {
                return (!(($input.val() === "" && defaultValue == null) && ($input.val() != defaultValue)) || parseInt($input.val()) == 0);
            };
            this.validate = function () {
                if (args.column.validator) {
                    var validationResults = args.column.validator($input.val(), $input);
                    if (!validationResults.valid) {
                        var row = $scope.grid.getData()[$scope.grid.getActiveCell().row];
                        if (args.column.field.toUpperCase().includes("QUANTITY")) {
                            row.Quantity = $input.val();
                        }
                        return validationResults;
                    }
                }

                return { valid: true, msg: null };
            };
            this.init();
        }

        //#endregion

        //#region Grid Validation

        $scope.requiredFieldValidator = function (value) {
            if ($scope.grid.getActiveCell() != null) {
                var rowData = $scope.grid.getData()[$scope.grid.getActiveCell().row];
                if (rowData.ItemId != undefined) {
                    if (value == null || value == undefined || !value.length) {
                        //toastr["warning"]("This is a required field");
                        return { valid: false, msg: "This is a required field" };
                    } else {
                        return { valid: true, msg: null };
                    }
                }
                else {
                    return { valid: true, msg: null };
                }
            }
            return {
                valid: true,
                msg: ""
            };
        }

        $scope.requiredSelectFieldValidator = function (value) {
            if (value == null || value == undefined || !value.length || value == "") {
                //toastr["warning"]("This is a required field");
                return { valid: false, msg: "This is a required field" };
            } else {
                return { valid: true, msg: null };
            }
        }

        $scope.requiredFieldIntegerValidator = function (value) {
            if ($scope.grid.getActiveCell() != null) {
                var rowData = $scope.grid.getData()[$scope.grid.getActiveCell().row];
                if (isNaN(value) || value == null || value == undefined || value.length <= 0) {
                    //toastr["warning"]("This is a required field");
                    return { valid: false, msg: "This is a required field" };
                }
                else if (parseInt(value) <= 0) {
                    //toastr["warning"]("Can't have 0 value", "Warning");
                    return {
                        valid: false,
                        msg: "Quantity can't be less than or equal to zero(0)."
                    };
                }
                else if (rowData.AvailableStock !== undefined) {
                    if (parseInt(value) > parseInt(rowData.AvailableStock)) {
                        CommonService.warningMessage("Item quantity must not be higher than available stock!");
                        return {
                            valid: false,
                            msg: "Quantity can't be greater than available stock."
                        };
                    }
                }
            }
            return {
                valid: true,
                msg: ""
            };
        }
        


        //#endregion

        //#region Modal

        function openModalSearch(linkType, searchText, args, navigateNext) {
            $uibModal.open({
                templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${linkType}`,
                controller: 'ChooseFromListCtrl',
                controllerAs: 'ctrl',
                windowClass: 'modal_style',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    modalData: function () {
                        return {
                            'LookupType': `${linkType}`,
                            'Title': 'search Item',
                            'ExcSelection': $scope.filter.requiresExclusion ? args.grid.getData().map(function (d) { return d.ItemId == null ? 0 : d.ItemId }) : null,
                            'SearchText': searchText,
                            'ProductId': $scope.filter.type == 'bom' ? $scope.m.ProductId : null,
                            'ItemType': $scope.filter.itemType,
                            'ItemId': args.item.ItemId
                        };
                    }
                }
            }).result.then(function (data) {
                var columns = args.grid.getColumns();

                switch ($scope.filter.linkType) {
                    case 'ITM':
                        args.item.ItemId = data.ItemMasterID;
                        args.item.ItemNo = data.ItemCode;
                        args.item.ItemName = data.ItemName;
                        args.item.Quantity = 1;

                        populateOptionsBasedOnItem(data, columns, args);

                        var warehouseDefault = data.ItemWarehouses.find(r => r.IsDefault);
                        if (warehouseDefault != null && warehouseDefault != undefined) {
                            args.item.WarehouseId = warehouseDefault.WarehouseId;
                            args.item.WarehouseCode = warehouseDefault.Code;
                            args.item.WarehouseName = warehouseDefault.Name;
                        }

                        var pricelistDefault = data.ItemPriceLists.find(r => r.IsDefault);
                        if (pricelistDefault != null && pricelistDefault != undefined) {
                            args.item.PriceListId = pricelistDefault.PriceListId;
                            args.item.PriceList = pricelistDefault.Name;
                            args.item.UnitPrice = pricelistDefault.Price.toFixed(2);
                            args.item.Total = (pricelistDefault.Price * args.item.Quantity).toFixed(2);
                        }

                        break;

                    case 'WHS':
                        args.item.WarehouseId = data.WarehouseId;
                        args.item.WarehouseCode = data.WhsCode;
                        args.item.WarehouseName = data.WhsName;

                        break;
                    case 'ITMWHS':
                        var itemAvailable = data.AvailableQty - args.item.Quantity;
                        if (data.AvailableQty > 0) {
                            if (itemAvailable < 0) {
                                CommonService.warningMessage("Item quantity must not be higher than available stock!");
                            }
                            else {
                                args.item.WarehouseId = data.WarehouseId;
                                args.item.WarehouseCode = data.WarehouseCode;
                                args.item.ItemWHS = data.WarehouseName;
                                args.item.WarehouseName = data.WarehouseName;
                                args.item.AvailableStock = data.AvailableQty;
                            }
                        }
                        else {
                            CommonService.warningMessage("No item available");
                        }

                        break;
                }

                if (navigateNext)
                    args.grid.navigateNext();
                $scope.addGridRow(); // add row

            }, function () {
                count = []
            });
        }

        function PopulateSelect(select, dataSource, addBlank) {
            var newOption;
            if (addBlank) { select.appendChild(new Option('--Please select--', '')); }
            $.each(dataSource, function (value, text) {
                newOption = new Option(text.Name, text.Name);
                select.appendChild(newOption);
            });
        };

        function populateOptionsBasedOnItem(data, columns, args) {
            for (var i = 0; i < columns.length; i++) {
                var column = columns[i];

                if (column.field.toUpperCase().includes("PRICELIST")) {
                    args.item.PriceListId = null;
                    args.item.PriceList = "";
                    args.item.PRICELISTDataSource = data.ItemPriceLists;
                }
                else if (column.field.toUpperCase().includes("WAREHOUSE")) {
                    args.item.WarehouseId = null;
                    args.item.WarehouseName = "";
                    args.item.WAREHOUSEDataSource = data.ItemWarehouses;
                }
                else if (column.field.toUpperCase().includes("UNITOFMEASURE")) {

                    ItemMasterService.GetItemUnitOfMeasuresByItemId({ id: args.item.ItemId, type: column.type }).then(function (d) {
                        args.item.UOMDataSource = d.DataResult;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });

                    args.item.UnitOfMeasureId = null;
                    args.item.UnitOfMeasure = "";
                }
                else if (column.field.toUpperCase().includes("ITEMWHS")) {
                    args.item.WarehouseId = null;
                    args.item.WarehouseName = "";
                    args.item.ItemWHS = "";
                }
            }
        }

        function PopulateOnSelect(args, item, state) {
            var data = args.item[args.column.name.replace(/\s/g, '') + "DataSource"].find(r => r.Name == state);

            if (args.column.field.toUpperCase().includes("PRICELIST")) {
                if (data != null) {
                    console.log(data);
                    item["PriceListId"] = data.PriceListId;
                    item[args.column.field] = data.Name;
                    item["UnitPrice"] = data.Price.toFixed(2);

                    if (item["Quantity"] != undefined) {
                        item["Total"] = (data.Price * parseInt(item["Quantity"])).toFixed(2);
                    }
                }
                else {
                    item["PriceListId"] = "";
                    item[args.column.field] = "";
                }
            }

            if (args.column.field.toUpperCase().includes("WAREHOUSE")) {

                if (data != null) {
                    item["WarehouseId"] = data.WarehouseId;
                    item["Available"] = data.Available;
                    item[args.column.field] = data.Name;
                }
                else {
                    item["WarehouseId"] = "";
                    item[args.column.field] = "";
                }
            }
            if (args.column.field.toUpperCase().includes("UNITOFMEASURE")) {

                if (data != null) {
                    item["UnitOfMeasureId"] = data.Id;
                    item[args.column.field] = data.Description;
                }
                else {
                    item["UnitOfMeasureId"] = "";
                    item[args.column.field] = "";
                }
            }
        }

        $scope.calculateTotalPrice = function (totalPrice, unitPrice, qty) {
            totalPrice = unitPrice != null ? ((unitPrice.replace(',', '') * 1) * (qty * 1)) : 0;
            totalPrice = totalPrice.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            return totalPrice;
        }
        //#endregion
    }]);