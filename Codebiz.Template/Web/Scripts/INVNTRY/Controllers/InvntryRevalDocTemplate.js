(function () {
    let doc = angular.module('cb.invntryreval.doctemplate', [
        'INVNTRY.DATASERVICE',
        'SL.DATASERVICE',
        'cb.components.linktextbox',
        'cb.components.SelectList'
    ])
    doc
        .controller('InvntryRevalDocTemplateCtrl', ['$scope',
            '$rootScope',
            '$filter',
            '$uibModal',
            '$window',
            '$timeout',
            '$q',
            'CommonService',
            'doc.formgrid.service',
            'ItemMasterService',
            'WarehouseService',
            'ItemGroupService',
            'invntry.docnum.service',
            'iqi.service',
            'iqc.service',
            'iqp.service',
            'imv.service',

            function (
                $scope,
                $rootScope,
                $filter,
                $uibModal,
                $window,
                $timeout,
                $q,
                $cs,
                $docFormGrid,
                $itm,
                $whs,
                $itg,
                $docNum,
                $iqi,
                $iqc,
                $iqp,
            ) {

                self = this;

                function setDocName(t) {
                    switch ($scope.objType) {
                        case 'IQI':
                            return `Opening Balances ${t}`
                        case 'IQC':
                            return `Physical Count ${t}`
                        case 'IQP':
                            return `Physical Count ${t}`
                        case 'IMV':
                            return `Inventory Revaluation ${t}`
                        default:
                            return '';
                    }
                }

                self.openModal = function (linkType, searchText, modal_size) {
                    return $uibModal.open({
                        templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${linkType}`,
                        controller: 'ChooseFromListCtrl',
                        controllerAs: 'ctrl',
                        windowClass: 'clsPopup',
                        backdrop: 'static',
                        keyboard: false,
                        size: modal_size,
                        resolve: {
                            modalData: function () {
                                return {
                                    'LookupType': `${linkType}`,
                                    'SearchText': `${searchText}`,
                                    'Title': 'Choose From List'
                                };
                            }
                        } // data passed to the controller
                    }).result
                        .then(function (d) {
                            return {
                                "linkType": linkType,
                                "data": d
                            }
                        },
                            function () { })
                }

                self.LinkTextBoxEditor = function LinkTextBoxEditor(args) {
                    var $input;
                    var $inputElement;
                    var defaultValue;
                    var newValue
                    var selectedValue
                    var scope = this;
                    var calendarOpen = false;
                    this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER];
                    this.init = function () {
                        $inputElement = $(`<div class="input-icon right">
                                            <i id="OBJ1002" name="OBJ1002" class="fa fa-search"  style="color:#32D5C2 !important; margin-top:1px !important;"></i>
                                            <input id="GOBJ1001" type="text" name="GOBJ1001" class="editor-text" value="" autocomplete="off" />
                                            </div>`);

                        $inputElement.appendTo(args.container);
                        $input = $(`input[name="GOBJ1001"]`).select()
                        $input.focus().select();


                        var linkType = ""

                        if (args.column.field === "ItemCode")
                            linkType = 'ITM'

                        if (args.column.field === "WhsCode")
                            linkType = 'WHS'

                        if (
                            args.column.field === "AcctName"
                        )
                            linkType = 'ACT'

                        if (args.column.field === "AcctCode"
                            || args.column.field === "COGSAcct"
                            || args.column.field === "InvntryOffsetIncAcct"
                            || args.column.field === "InvntryOffsetDecAcct"
                            || args.column.field === "InvntryRevalIncAcct"
                            || args.column.field === "InvntryRevalDecAcct"


                        )
                            linkType = 'ACT'


                        $("i[name='OBJ1002']").on('click', function (e) {
                            $q.all([self.openModal(linkType, '', 'md')]).then(function (d) {
                                if (d) {
                                    if (linkType === 'ITM') {
                                        $input.val(d[0].data.ItemCode)
                                        newValue = d[0].data.ItemCode
                                        selectedValue = d[0].data
                                    }

                                    if (linkType === 'WHS') {
                                        $input.val(d[0].data.WhsCode)
                                        newValue = d[0].data.WhsCode
                                        selectedValue = d[0].data
                                    }

                                    if (linkType === 'ACT') {
                                        $input.val(d[0].data.AcctCode)
                                        newValue = d[0].data.AcctCode
                                        selectedValue = d[0].data
                                    }
                                }
                            })
                        })

                        $("input[name='GOBJ1001']").on('keydown', (e) => {
                            //console.log($("input[name='GOBJ1001']").val())
                        });
                    };

                    this.destroy = function () {
                        $inputElement.remove();
                    };
                    this.focus = function () {
                        $input.focus();
                    };
                    this.loadValue = function (item) {
                        defaultValue = item[args.column.field];
                        $input.val(defaultValue);
                        $input[0].defaultValue = defaultValue;
                        $input.select();
                    };
                    this.serializeValue = function () {
                        return $input.val();
                    };
                    this.applyValue = function (item, state) {
                        item[args.column.field] = state;

                        if (args.column.field === 'ItemCode')
                            if (selectedValue)
                                //item['Dscription'] = selectedValue.ItemName
                                item['ItemName'] = selectedValue.ItemName
                    };
                    this.isValueChanged = function () {
                        return ($input.val() != defaultValue);
                    };
                    this.validate = function () {
                        return {
                            valid: true,
                            msg: null
                        };
                    };
                    this.init();
                }

                self.SelectListEditor = function SelectListEditor(args) {
                    var $input;
                    var defaultValue;
                    var scope = this;
                    var calendarOpen = false;
                    this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER];


                    var populateSelect = function (select, dataSource, addBlank) {
                        var index, len, newOption;
                        if (addBlank) { select.appendChild(new Option('', '')); }

                        if (dataSource) {
                            if (args.column.name === 'UOM') {
                                dataSource.forEach(x => {
                                    newOption = new Option(x['LookupName'], x['LookupCode']);
                                    select.appendChild(newOption);
                                })
                            }
                            else {
                                dataSource.forEach(x => {
                                    newOption = new Option(x['LookupName'], x['LookupCode']);
                                    select.appendChild(newOption);
                                })
                            }
                        }
                    };

                    this.init = function () {
                        $inputElement = $(`<div><select id="OBJ0001"><option value="">--Please select--</option></select></div>`);

                        $inputElement.appendTo(args.container);

                        $inputElement.focus().select();

                        $input = $(`select[id="OBJ0001"]`).select();

                        //populateSelect($input[0], $scope.vatGroupList)
                        populateSelect($input[0], args.column.dataSource)

                        $input.select2({ allowClear: true }).val(args.item[args.column.field]).trigger('change')
                        //$input.val(args.item[args.column.field])

                        //if (args.item[args.column.field]) {
                        //    $input.select2('val', args.item[args.column.field])
                        //}


                        $inputElement.children('span.select2.select2-container.select2-container--bootstrap')
                            .children('span.selection')
                            .children('span.select2-selection.select2-selection--single')
                            .attr('style', `background-color:#fff !important; border: none;!important; color:#555 !important; outline:0; width:100%; height:inherit; padding-top:1px ;padding-bottom:1px;padding-left:1px`)
                    };
                    this.destroy = function () {
                        //$input.autocomplete("destroy");
                        $input.select2('close');
                        $input.select2('destroy');
                        $inputElement.remove();
                    };
                    this.focus = function () {
                        $input.focus();
                    };
                    this.loadValue = function (item) {
                        defaultValue = item[args.column.field];
                        $input.val(defaultValue);
                        //$input[0].defaultValue = defaultValue;
                        $input.select();
                    };
                    this.serializeValue = function () {
                        return $input.val();
                    };
                    this.applyValue = function (item, state) {
                        item[args.column.field] = state;
                    };
                    this.isValueChanged = function () {
                        return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
                    };
                    this.validate = function () {
                        return {
                            valid: true,
                            msg: null
                        };
                    };
                    this.init();
                }

                $q.all([
                    $docFormGrid.GetColumns($scope.objType, 'I').then(d => { return d.DataResult }),
                    $docNum.GetDocNum($scope.objType).then(d => { return d.StatusCode === '0' ? d.DataResult : null }),
                    $scope.docData.DocAction,
                ]).then((qres => {

                    self.ColData = qres[0]

                    $scope.DocSeriesList = qres[1].DocSeriesList

                    self.Action = qres[2].Action

                    $scope.g = {}

                    $scope.doc = {}

                    $scope.doc.CanEditGrid = 'N'

                    $scope.m = {}

                    $scope.g.gridData = []

                    self.AddNewLineData = function () {
                        $scope.g.gridData.push({
                            "DocLineID": "0",
                            "DocID": "0",
                            "LineNum": $scope.g.gridData.length + 1,
                            "ObjType": "",
                            "ItemCode": "",
                            "ItemName": "",
                            "OnHandQtyBef": "",
                            "Quantity": "",
                            "Price": "",
                            "Rate": "",
                            "WhsCode": "",
                            "InvntryUOM": "",
                            "InvntryOffsetIncAcct": "",
                            "InvntryOffsetDecAcct": "",
                            "DocTotal": "",
                            "DocTotalFC": "",
                            "DocTotalSC": "",
                            "OcrCode": "",
                            "OcrCode1": "",
                            "OcrCode2": "",
                            "OcrCode3": "",
                            "OcrCode4": "",
                            "Project": ""
                        })
                    }

                    $scope.onBtnSaveClick = (e) => {
                        let data = {
                            'objType': $scope.objType,
                            'docType': $scope.m.DocType, // Item or service
                            'docHeader': $scope.m,
                            'docDetail': $scope.g.gridData,
                            //'docSerialNoDetail': $scope.s.SerialNumberList
                        }

                        $scope.$emit('onDocumentSaveAction', data)
                    }

                    $scope.onBtnCancelClick = (e) => { }

                    let UpdateColumnEditor = function (d) {

                        if (d.originalname === 'LineNum') {
                            d.headerCssClass = 'text-center'
                            d.cssClass = 'text-center'
                        }

                        if (d.originalname === 'ItemCode') {
                            if (self.Action === 'ADD')
                                d.editor = self.LinkTextBoxEditor
                        }

                        if (d.originalname === 'Dscription' || d.originalname === 'ItemName') {
                            //d.name = 'Item Name'
                            d.width = 350
                        }

                        if (d.originalname === 'Price' || d.originalname === 'PriceAfVat') {

                            if (self.Action === 'ADD') {
                                d.editor = Slick.Editors.Float
                            }

                            d.cssClass = 'text-right'
                            d.headerCssClass = 'text-center'
                        }


                        if (d.originalname === 'PriceDiff' || d.originalname === 'LineTotal' || d.originalname === 'StockPriceBef') {
                            d.cssClass = 'text-right'
                            d.headerCssClass = 'text-center'
                        }


                        if (d.originalname === 'QtyDiff' || d.originalname === 'OnHandQty') {

                            d.cssClass = 'text-right'
                            d.headerCssClass = 'text-center'
                        }


                        if (d.originalname === 'WhsCode') {
                            d.name = 'WAREHOUSE'
                            if (self.Action === 'ADD') {
                                d.editor = self.LinkTextBoxEditor
                            }

                            d.headerCssClass = 'text-center'
                        }


                        if (d.originalname === 'Quantity') {

                            if (self.Action === 'ADD') {
                                d.editor = Slick.Editors.Float
                            }

                            d.cssClass = 'text-right'
                            d.headerCssClass = 'text-center'
                        }


                        if (d.originalname === 'InvntryUOM') {
                            d.headerCssClass = 'text-center'
                            d.width = 150
                        }

                        if (d.originalname === 'OcrCode' || d.originalname === 'Project') {
                            d.headerCssClass = 'text-center'
                        }



                        //InvntryRevalIncAcct
                        //InvntryRevalDecAcct
                        if (d.originalname === 'AcctCode'
                            || d.originalname === 'COGSAcct'
                            || d.originalname === 'InvntryRevalIncAcct'
                            || d.originalname === 'InvntryRevalDecAcct'
                        ) {
                            if (self.Action === 'ADD') {
                                d.editor = self.LinkTextBoxEditor
                            }
                            d.headerCssClass = 'text-center'
                            d.width = 150
                        }

                        return d;
                    }

                    let UpdateColumns = function (col_list) {
                        $scope.g.gridColumns = []
                        col_list.filter(x => x.ShowColumn === 'Y').forEach(d => {
                            $scope.g.gridColumns.push(UpdateColumnEditor(d))
                        })
                    }

                    UpdateColumns(self.ColData)

                    if (self.Action === 'ADD') {

                        self.AddNewLineData()
                        $scope.doc.CanEditGrid = "Y"
                        $scope.doc.CanEditDocDate = "Y"
                        $scope.doc.CanEditTaxDate
                        $scope.m.Series = "Default"
                        $scope.m.DocNum = qres[1].DocNextNum
                        $scope.m.RevalType = "P"
                    }

                    /*events received*/
                    $scope.$on('onGridKeyDown', function (e, args) {

                        let grid = args.grid,
                            gridData = grid.getData(),
                            gridItemData = grid.getDataItem(args.row),
                            field = grid.getColumns()[args.cell].field;

                        //if ($scope.m.DocType === 'I') {

                        let itemCode = gridItemData['ItemCode']

                        let setGridItemData = function (d) {
                            gridItemData['ItemName'] = d.ItemMaster.ItemName
                            gridItemData['Quantity'] = 1
                            gridItemData['UOM'] = d.ItemMaster.PurchasingUoM
                            gridItemData['NumPerMsr'] = d.ItemMaster.ItemSalesPerUnit !== undefined ? d.ItemMaster.ItemSalesPerUnit : 0
                            gridItemData['InvntryUOM'] = d.ItemMaster.InvntryUOM

                            //if GLAccount is set by itemGroup
                            if (d.ItemMaster.GLAccountSetupBy === 'G') {
                                $itg.GetItemGroupByID(d.ItemMaster.ItemGroupId).then(iRes => {
                                    if (iRes.StatusCode === '0') {
                                        let itg = iRes.DataResult
                                        gridItemData['InvntryRevalIncAcct'] = itg.GLOffsetIncAcct
                                        gridItemData['InvntryRevalDecAcct'] = itg.GLOffsetDecAcct
                                        grid.invalidate();
                                    }
                                })
                            }

                            //warehouse
                            let dfWarehouse = d.ItemMaster.ItemWarehouses.filter(q => q.IsDefault)[0]
                            if (dfWarehouse) {
                                $whs.GetWarehouseByID(dfWarehouse.WarehouseId).then(r2 => {
                                    if (r2.StatusCode === '0') {

                                        let whs = r2.DataResult

                                        //if GLAccount is set by warehouse
                                        if (d.ItemMaster.GLAccountSetupBy === 'W') {
                                            gridItemData['InvntryRevalIncAcct'] = whs.GLOffsetIncAcct
                                            gridItemData['InvntryRevalDecAcct'] = whs.GLOffsetDecAcct
                                        }

                                        gridItemData['WhsCode'] = whs.WhsCode
                                        gridItemData['OnHandQty'] = $filter('number')(dfWarehouse.OnHand, '2')
                                        gridItemData['StockPriceBef'] = $filter('number')(dfWarehouse.ItemCost, '2')
                                        gridItemData['Price'] = $filter('number')(dfWarehouse.ItemCost, '2')
                                        grid.invalidate();
                                    }
                                })
                            }

                            self.AddNewLineData()
                        }

                        // get default values
                        if (field === 'ItemCode' || field === 'ItemName') {
                            $itm.GetItemByCodeDocLookup(itemCode).then(res => {
                                d = res.DataResult

                                if (res.StatusCode === '0') {
                                    setGridItemData(d)

                                    grid.invalidate()
                                }

                                if (res.StatusCode === '-2') { // item not found
                                    $q.all([self.openModal('ITM', itemCode)]).then(itmRes => {
                                        modalData = itmRes[0].data
                                        $itm.GetItemByCodeDocLookup(modalData.ItemCode).then(res3 => {
                                            if (res3.StatusCode === '0') {
                                                var item_data = res3.DataResult
                                                gridItemData['ItemCode'] = item_data.ItemMaster.ItemCode
                                                setGridItemData(item_data)
                                                grid.invalidate()
                                            }
                                        })
                                    })
                                }
                            })
                        }

                        if (field === 'WhsCode') {
                            let whsCode = gridItemData['WhsCode']
                            $whs.GetWarehouseByCode(whsCode).then((wResult) => {
                                if (wResult.StatusCode === '0') {
                                    let whs = wResult.DataResult

                                    $itm.GetItemWarehouseByCode(gridItemData['ItemCode'], whs.WhsCode).then(r => {
                                        if (r.StatusCode === '0') {
                                            let itw = r.DataResult
                                            gridItemData['WhsCode'] = whs.WhsCode
                                            gridItemData['OnHandQty'] = $filter('number')(itw.OnHand, '2')
                                            gridItemData['StockPriceBef'] = $filter('number')(itw.ItemCost, '2')
                                            gridItemData['Price'] = $filter('number')(itw.ItemCost, '2')
                                            grid.invalidate();
                                        }
                                    })
                                }

                                if (wResult.StatusCode === '-2') { // notfound
                                    $q.all([self.openModal('WHS', whsCode)]).then(function (modalRes) {
                                        whsData = modalRes[0].data
                                        //gridItemData['WhsCode'] = whsData.WhsCode
                                        //grid.invalidate();
                                        $itm.GetItemWarehouseByCode(gridItemData['ItemCode'], whsData.WhsCode).then(r => {
                                            if (r.StatusCode === '0') {
                                                let itw = r.DataResult
                                                gridItemData['WhsCode'] = whsData.WhsCode
                                                gridItemData['OnHandQty'] = $filter('number')(itw.OnHand, '2')
                                                gridItemData['StockPriceBef'] = $filter('number')(itw.ItemCost, '2')
                                                gridItemData['Price'] = $filter('number')(itw.ItemCost, '2')
                                                grid.invalidate();
                                            }
                                        })
                                    })
                                }
                            })
                        }

                        if (field === 'Price') {
                            //todo
                            let stockPrice = gridItemData['StockPriceBef'].replace(',', '')
                                , price = gridItemData['Price'].replace(',', '')
                                , priceDiff = 0
                                , lineTotal = 0
                                , onHandQty = gridItemData['OnHandQty'].replace(',', '')


                            priceDiff = price - stockPrice

                            lineTotal = priceDiff * onHandQty

                            gridItemData['PriceDiff'] = $filter('number')(priceDiff, '2')

                            gridItemData['LineTotal'] = $filter('number')(lineTotal, '2')

                            grid.invalidate()
                        }
                        //}
                    })

                    /*grid options*/
                    $scope.g.withUpdateCol = 'N'

                    $scope.g.gridOptions = {
                        enableCellNavigation: true,
                        enableColumnReorder: false,
                        editable: $scope.doc.CanEditGrid === 'Y',
                        autoEdit: true,
                        enableAddRow: false,
                        asyncEditorLoading: false,
                        createFooterRow: true,
                        //showFooterRow: true,
                        footerRowHeight: 28,
                        enableAutoSizeColumns: true,
                    };
                }))



            }])
        .directive('cbDocTmpl', function ($http) {
            return {
                restrict: 'EA',
                controller: 'InvntryRevalDocTemplateCtrl',
                scope: {
                    cLabel: '@cLabel',
                    objType: '@objType',
                    docData: '=docData'
                },
                link: function (scope, element, attrs) { },
                templateUrl: `DocumentFormTmpl.html`
            }
        });
})();