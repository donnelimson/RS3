(function () {
    var doc = angular.module('cb.invntry.doctemplate', ['FIN.DATASERVICE', 'BP.SETUP.SERVICE', 'INVNTRY.DATASERVICE', 'SL.DATASERVICE', 'TRN.INVNTRY.SERVICE'])
    doc.controller('INVNTRYDocTemplateCtrl',
        [   '$scope',
            '$rootScope',
            '$filter',
            '$uibModal',
            '$window',
            '$timeout',
            '$q',
            'CommonService',
            'BusinessPartnerService',
            'ItemMasterService',
            'TaxGroupService',
            'WarehouseService',
            'ItemSerialTransactionService',
            'doc.formgrid.service',
            'cfl.bp.service',
            'cfl.invntry.service',
            'cfl.fin.service',
            'SelectListService',
            'invntry.docnum.service',
            'ItemPackagingService',
            'ige.service',
            'ign.service',
            function (
                $scope,
                $rootScope,
                $filter,
                $uibModal,
                $window,
                $timeout,
                $q,
                $cs,
                $bp,
                $itm,
                $tg,
                $whs,
                $itemSerialTrn,
                $docFormGrid,
                $cflBPService,
                cflInvnrtyService,
                cflFinService,
                $selectService,
                $docNum,
                $itmPackService,
                $ige,
                $ign,
            ) {
                var self = this

                $scope.docName = ''

                //$scope.taxCategory = 'O'

                $scope.RecordMode = 'A'

                switch ($scope.objType) {
                    case 'SQU':
                        $scope.taxCategory = 'O'; break;
                    case 'SOR':
                        $scope.taxCategory = 'O'; break;
                    case 'SDN':
                        $scope.taxCategory = 'O'; break;
                    case 'SRD':
                        $scope.taxCategory = 'O'; break;
                    case 'SIN':
                        $scope.taxCategory = 'O'; break;
                    case 'SCR':
                        $scope.taxCategory = 'O'; break;
                    case 'PQU':
                        $scope.taxCategory = 'I'; break;
                    case 'POR':
                        $scope.taxCategory = 'I'; break;
                    case 'PDN':
                        $scope.taxCategory = 'I'; break;
                    case 'PRD':
                        $scope.taxCategory = 'I'; break;
                    case 'PIN':
                        $scope.taxCategory = 'I'; break;
                    case 'PCR':
                        $scope.taxCategory = 'I'; break;
                    default:
                        $scope.taxCategory = 'O';
                        break;
                }

                function setDocName(t) {
                    switch ($scope.objType) {
                        case 'IGE':
                            return `Goods Issue ${t}`
                        case 'IGN':
                            return `Goods Receipt ${t}`
                        default:
                            return '';
                    }
                }

                self.loadBPDefaults = function (cardCode) {
                    $bp.GetBusinessPartnerByCode(cardCode).then(function (d) {
                        if (d.DataResult) {
                            $scope.bp = d.DataResult
                            $scope.ShipToList = []
                            $scope.BillToList = []

                            $scope.m.PaymentTermID = $scope.bp.PaymentTermID // set payment terms 

                            $scope.m.ControlAccount = $scope.bp.CtlDebCredPayAcct

                            $scope.bp.BPAddresses.forEach(x => {
                                if (x.AddressType === 'D') {
                                    $scope.ShipToList.push({
                                        'LoookupID': x.BusinessPartnerAddressID,
                                        'LookupCode': x.AddressCode,
                                        'LookupName': x.AddressCode
                                    })

                                    if (x.IsDefault === 'Y') {
                                        $scope.m.ShipToCode = x.AddressCode
                                        $scope.m.ShipToAddr = `${x.Block} ${x.StreetNo} ${x.Street}\n${x.City}\n${x.Country} ${x.PostalCode}`
                                    }
                                }
                                if (x.AddressType === 'B') {
                                    $scope.BillToList.push({
                                        'LoookupID': x.BusinessPartnerAddressID,
                                        'LookupCode': x.AddressCode,
                                        'LookupName': x.AddressCode
                                    })
                                    if (x.IsDefault === 'Y') {
                                        $scope.m.BillToCode = x.AddressCode
                                        $scope.m.BillToAddr = `${x.Block} ${x.StreetNo} ${x.Street}\n${x.City}\n${x.Country} ${x.PostalCode}`
                                    }
                                }
                            })

                            $tg.GetTaxGroupByID($scope.bp.VatGroupID).then((tg) => {
                                if (tg.StatusCode === '0') {
                                    $scope.tg = tg.DataResult
                                }
                            })


                            if ($scope.recordMode === 'A') {
                                self.SetGridEditStatus(true)
                            }
                        }
                    })
                }

                $rootScope.$on('onUpdateLinkTextBox_item_0001', function (e, args) {
                    if (args) {
                        if (args.CardCode) {
                            $scope.m.CardCode = args.CardCode
                            $scope.m.CardName = args.CardName
                            $scope.docName = setDocName(`(${$scope.m.CardName})`)
                            self.loadBPDefaults(args.CardCode)
                        }
                    }
                });

                $rootScope.$on('onUpdateLinkTextBox_item_0040', function (e, args) {
                    if (args) {
                        console.log(args)
                    }
                });

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

                        if (args.column.field === "AcctCode")
                            linkType = 'ACT'

                        if (args.column.field === "AcctName")
                            linkType = 'ACT'

                        if (args.column.field === "COGSAcct")
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
                                item['Dscription'] = selectedValue.ItemName
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
                    $selectService.GetTaxGroupLookupByCategorySL($scope.taxCategory).then((d) => { return d; }),
                    $scope.docData.model,
                    $docFormGrid.GetColumns('INVNTRY','I').then((d) => { return d.DataResult }),
                    $docNum.GetDocNum($scope.objType).then(d => { return d.StatusCode === '0' ? d.DataResult : null }),
                    $scope.docData.DocAction,
                ]).then(
                    function (res) {
                        console.log(res);
                        self.ColData = res[2]
                        self.Action = res[4].Action

                        $scope.g = {}
                        $scope.g.gridOptions = {}
                        $scope.m = {}
                        $scope.doc = {}
                        $scope.doc.CanEditCardCode = 'N'
                        $scope.doc.CanEditDocDate = 'N'
                        $scope.doc.CanEditDocDueDate = 'N'
                        $scope.doc.CanEditDocumentDate = 'N'
                        $scope.doc.CanEditGrid = 'Y'

                        self.SetGridEditStatus = function (c) {
                            if (c) {
                                $scope.g.gridOptions.editable = true
                                $scope.g.withUpdateCol = 'Y'
                            }
                            else {
                                $scope.g.gridOptions.editable = false
                                $scope.g.withUpdateCol = 'Y'
                            }
                        }

                        $scope.vatGroupList = res[0].DataResult
                        $scope.m = res[1]
                        $scope.g.gridRecordOption = {
                            "CanAdd": "N",
                            "CanEdit": "N",
                            "CanDelete": "N"
                        };

                        if (self.Action === 'COPY') {
                            $scope.recordMode = 'A'
                            $scope.s = {}
                            $scope.s.SerialNumberList = []
                            $scope.m.DocDate = $filter('date')($scope.m.DocDate, 'MM/dd/yyyy');
                            $scope.m.DocDueDate = $filter('date')($scope.m.DocDueDate, 'MM/dd/yyyy');
                            $scope.m.TaxDate = $filter('date')($scope.m.TaxDate, 'MM/dd/yyyy');
                            $scope.m.Remarks = `Based on ${$scope.m.DocNum}`
                            self.loadBPDefaults($scope.m.CardCode)

                            /*delete the 0 open qty*/

                            for (let x = 0; x < $scope.m.Document_Lines.length; x++) {
                                if ($scope.m.Document_Lines[x].OpenQty * 1.0 <= 0) {
                                    $scope.m.Document_Lines.splice(x, 1)
                                }
                            }

                            $scope.m.Document_Lines.map(x => {
                                x.DocLineID = 0
                                x.DocID = 0
                                x.BaseLine = x.LineNum
                                x.BaseEntry = res[4].SrcID
                                x.BaseType = res[4].SrcType
                                x.Quantity = x.OpenQty
                            })

                            $scope.m.DocID = -1

                            $scope.g.gridRecordOption = {
                                "CanAdd": "Y",
                                "CanEdit": "Y",
                                "CanDelete": "Y"
                            };
                        }

                        $scope.m.DocnumType = "A"

                        $scope.g.gridData = []

                        // set BP 
                        if ($scope.m.DocID > 0) {
                            $scope.recordMode = 'E'
                            $scope.s = {}
                            $scope.s.SerialNumberList = []
                            self.loadBPDefaults($scope.m.CardCode)
                            $scope.m.DocDate = $filter('date')($scope.m.DocDate, 'MM/dd/yyyy');
                            $scope.m.DocDueDate = $filter('date')($scope.m.DocDueDate, 'MM/dd/yyyy');
                            $scope.m.TaxDate = $filter('date')($scope.m.TaxDate, 'MM/dd/yyyy');
                            $scope.DocSeriesList = res[3].DocSeriesList
                            $scope.s.SerialNumberList = $scope.m.Document_SerialNoLines
                        }

                        else {
                            $scope.recordMode = 'A'
                            $scope.s = {}
                            $scope.s.SerialNumberList = []
                            $scope.m.DocNum = res[3].DocNextNum
                            $scope.DocSeriesList = res[3].DocSeriesList
                            $scope.m.Series = res[3].DocSeriesList.filter(x => x.IsDefault)[0].LookupCode
                        }

                        if ($scope.m.Document_Lines.length > 0) {
                            let docTotalBeforeVat = 0,
                                vatSum = 0,
                                discountSum = 0,
                                lineCounter = 0

                            $scope.m.Document_Lines.forEach(x => {
                                if ($scope.m.DocType === 'I') {
                                    x.LineTotal = x.Price * x.Quantity
                                    x.GTotal = x.PriceAfVat * x.Quantity
                                }
                                if ($scope.m.DocType === 'S') {
                                    x.LineTotal = x.Price
                                    x.GTotal = x.PriceAfVat
                                }
                                x.LineNum = ++lineCounter;
                                $scope.g.gridData.push(x)
                                docTotalBeforeVat += x.LineTotal;
                                vatSum += x.VatSum * 1;
                                discountSum += x.DiscountAmt;
                                $scope.m.TotalBeforeVat = docTotalBeforeVat;
                                $scope.m.DiscountSum = discountSum;
                                $scope.m.VatSum = vatSum;
                                $scope.m.DocTotal = docTotalBeforeVat * 1 + vatSum * 1
                            })
                        }

                        let UpdateColumnEditor = function (d) {

                            if ($scope.recordMode === 'A' && self.Action === 'ADD') {
                                $scope.canAddEdit = true
                            }

                            if ($scope.recordMode === 'A' && self.Action === 'COPY') {
                                if ($scope.objType === 'PQU' || $scope.objType === 'POR')
                                    $scope.canAddEdit = true
                                else
                                    $scope.canAddEdit = false
                            }

                            if ($scope.recordMode === 'E' && self.Action === 'EDIT') {
                                if ($scope.objType === 'PQU' || $scope.objType === 'POR')
                                    $scope.canAddEdit = true
                                else
                                    $scope.canAddEdit = false
                            }

                            if ($scope.m.DocType === 'I') { //item Type

                                if (d.originalname === 'LineNum') {
                                    d.headerCssClass = 'text-center'
                                    d.cssClass = 'text-center'
                                }

                                if (d.originalname === 'LineTotal' || d.originalname === 'LineTotalFC' || d.originalname === 'GTotal' || d.originalname === 'GTotalFC') {
                                    d.cssClass = 'text-right'
                                    d.headerCssClass = 'text-center'
                                }

                                if (d.originalname === 'ItemCode') {
                                    if ($scope.canAddEdit)
                                        d.editor = self.LinkTextBoxEditor
                                }

                                if (d.originalname === 'Dscription') {
                                    //d.name = 'Item Name'
                                    d.width = 350
                                }

                                if (d.originalname === 'Price' || d.originalname === 'PriceAfVat') {

                                    if ($scope.canAddEdit) {
                                        d.editor = Slick.Editors.Text
                                    }

                                    d.cssClass = 'text-right'
                                    d.headerCssClass = 'text-center'
                                }

                                if (d.originalname === 'Discount') {
                                    if ($scope.canAddEdit) {
                                        d.editor = Slick.Editors.Text
                                    }

                                    d.cssClass = 'text-right'
                                    d.headerCssClass = 'text-center'
                                }

                                if (d.originalname === 'Quantity') {

                                    //if ($scope.canAddEdit) {
                                    //    d.editor = Slick.Editors.Text
                                    //}
                                    d.editor = Slick.Editors.Text
                                    d.cssClass = 'text-right'
                                    d.headerCssClass = 'text-center'
                                }

                                if (d.originalname === 'WhsCode') {
                                    d.name = 'WAREHOUSE'
                                    if ($scope.canAddEdit) {

                                        d.editor = self.LinkTextBoxEditor
                                    }
                                    d.headerCssClass = 'text-center'
                                }

                                if (d.originalname === 'VatGroup') {
                                    if ($scope.canAddEdit) {
                                        d.editor = self.SelectListEditor
                                        d.dataSource = $scope.vatGroupList
                                    }
                                    d.headerCssClass = 'text-center'
                                    d.width = 150
                                }


                                if (d.originalname === 'UOM') {
                                    if ($scope.canAddEdit) {
                                        d.editor = self.SelectListEditor
                                        d.dataSource = $scope.uomList
                                    }
                                    d.headerCssClass = 'text-center'
                                    d.width = 150
                                }

                                if (d.originalname === 'AcctCode' || d.originalname === 'COGSAcct') {
                                    if ($scope.canAddEdit) {
                                        d.editor = self.LinkTextBoxEditor
                                    }
                                    d.headerCssClass = 'text-center'
                                }
                                return d;
                            }

                            if ($scope.m.DocType === 'S') { //service Type 

                                if (d.originalname === 'LineNum') {
                                    d.headerCssClass = 'text-center'
                                    d.cssClass = 'text-center'
                                }

                                if (d.originalname === 'LineTotal' || d.originalname === 'LineTotalFC' || d.originalname === 'GTotal' || d.originalname === 'GTotalFC') {
                                    // if ($scope.canAddEdit) {
                                    //     d.editor = Slick.Editors.Text
                                    // }
                                    d.cssClass = 'text-right'
                                    d.headerCssClass = 'text-center'
                                }


                                if (d.originalname === 'Dscription') {
                                    if ($scope.canAddEdit) {
                                        d.editor = Slick.Editors.Text
                                    }
                                    d.width = 350
                                }

                                if (d.originalname === 'Price' || d.originalname === 'PriceAfVat') {
                                    if ($scope.canAddEdit) {
                                        d.editor = Slick.Editors.Text
                                    }

                                    d.cssClass = 'text-right'
                                    d.headerCssClass = 'text-center'
                                }

                                if (d.originalname === 'Discount') {
                                    if ($scope.canAddEdit) {
                                        d.editor = Slick.Editors.Text
                                    }

                                    d.cssClass = 'text-right'
                                    d.headerCssClass = 'text-center'
                                }

                                if (d.originalname === 'VatGroup') {
                                    if ($scope.canAddEdit) {
                                        d.editor = self.SelectListEditor
                                        d.dataSource = $scope.vatGroupList
                                    }
                                    d.headerCssClass = 'text-center'
                                    d.width = 150
                                }

                                if (d.originalname === 'AcctCode' || d.originalname === 'COGSAcct') {
                                    if ($scope.canAddEdit) {
                                        d.editor = self.LinkTextBoxEditor
                                    }
                                    d.headerCssClass = 'text-center'
                                    d.width = 150
                                }

                                if (d.originalname === 'AcctName') {
                                    d.headerCssClass = 'text-center'
                                    d.width = 350
                                }

                                return d;
                            }
                        }

                        let UpdateColumns = function (col_list) {
                            $scope.g.gridColumns = []
                            col_list.filter(x => x.ShowColumn === 'Y').forEach(d => {
                                $scope.g.gridColumns.push(UpdateColumnEditor(d))
                            })
                        }

                        UpdateColumns(self.ColData)

                        self.AddNewLineData = function () {
                            $scope.g.gridData.push
                                ({
                                    //$id: $scope.$id,
                                    "DocNum": "",
                                    "LineNum": $scope.g.gridData.length + 1,
                                    "VisNum": "",
                                    "LineType": "",
                                    "Text": "",
                                    "ObjType": "",
                                    "BaseEntry": "",
                                    "BaseType": "",
                                    "BaseLine": "",
                                    "BaseDocNum": "",
                                    "BaseDocRef": "",
                                    "TargetEntry": "",
                                    "LineStatus": "",
                                    "TargetType": "",
                                    "TargetLine": "",
                                    "TargetDocRef": "",
                                    "Currency": "",
                                    "CardCode": "",
                                    "CardName": "",
                                    "DocDate": "",
                                    "ItemCode": "",
                                    "Dscription": "",
                                    "Dscription2": "",
                                    "WhsCode": "",
                                    "Quantity": "",
                                    "OpenQty": "",
                                    "QtyPerPacUOM": "",
                                    "BaseQty": "",
                                    "BaseOpenQty": "",
                                    "QtyToShip": "",
                                    "DeliveredQty": "",
                                    "OrderedQty": "",
                                    "NumPerMsr": "",
                                    "ShipDate": "",
                                    "ShipToCode": "",
                                    "ShipToDesc": "",
                                    "UOM": "",
                                    "TotalInvntryQty": "",
                                    "StockPrice": "",
                                    "StockValue": "",
                                    "INMPRice": "",
                                    "PickQty": "",
                                    "PickStatus": "",
                                    "InvntryUOM": "",
                                    "ItemCost": "",
                                    "Price": "",
                                    "PriceAfVat": "",
                                    "Discount": "",
                                    "DiscountAmt": "",
                                    "Discount2": "",
                                    "DiscountAmt2": "",
                                    "Discount3": "",
                                    "DiscountAmt3": "",
                                    "Discount4": "",
                                    "DiscountAmt4": "",
                                    "Discount5": "",
                                    "DiscountAmt5": "",
                                    "PriceBeforeDscnt": "",
                                    "VatGroup": $scope.tg ? $scope.tg.VatCode : "",
                                    "VatPercent": $scope.tg ? $scope.tg.Rate : 0,
                                    "VatSum": "",
                                    "VatSumSys": "",
                                    "VatSumFC": "",
                                    "FinacialPeriodID": "",
                                    "AcctCode": "",
                                    "COGSAcct": "",
                                    "LineTotal": "",
                                    "LineTotalFC": "",
                                    "LineTotalSC": "",
                                    "LineVatSum": "",
                                    "LinVatSys": "",
                                    "LineVatFC": "",
                                    "OpenSum": "",
                                    "OpenSumFC": "",
                                    "Factor1": "",
                                    "Factor2": "",
                                    "Factor3": "",
                                    "Factor4": "",
                                    "Length1": "",
                                    "Len1Unit": "",
                                    "Width1": "",
                                    "Width1Unit": "",
                                    "Height1": "",
                                    "Height1Unit": "",
                                    "Length2": "",
                                    "Len2Unit": "",
                                    "Width2": "",
                                    "Width2Unit": "",
                                    "Height2": "",
                                    "Height2Unit": "",
                                    "Volume": "",
                                    "VolUnit": "",
                                    "ProjectCode": "",
                                    "OcrCode": "",
                                    "OcrCode2": "",
                                    "OcrCode3": "",
                                    "OcrCode4": "",
                                    "OcrCode5": "",
                                    "GTotal": "",
                                    "GTotalFC": "",
                                    "GrossProfit": "",
                                    "GrossProfitFC": "",
                                    "VersionNo": "",
                                })
                        }

                        //new record
                        if ($scope.recordMode === 'A' && self.Action === 'ADD') {
                            $scope.doc.CanEditCardCode = 'Y'
                            $scope.doc.CanEditDocDate = 'Y'
                            $scope.doc.CanEditDocDueDate = 'Y'
                            $scope.doc.CanEditDocumentDate = 'Y'
                            self.AddNewLineData()
                        }

                        //new record copy from/to
                        if ($scope.recordMode === 'A' && self.Action === 'COPY') {
                            $scope.doc.CanEditCardCode = 'Y'
                            $scope.doc.CanEditDocDate = 'Y'
                            $scope.doc.CanEditDocDueDate = 'Y'
                            $scope.doc.CanEditDocumentDate = 'Y'

                            if ($scope.objType === 'POR') {
                                $scope.doc.CanEditGrid = 'Y'
                                self.AddNewLineData()
                            }
                            else {
                                $scope.doc.CanEditGrid = 'N'
                            }
                        }

                        //edit record
                        if ($scope.recordMode === 'E' && self.Action === 'EDIT') {
                            $scope.doc.CanEditCardCode = 'Y'
                            $scope.doc.CanEditDocDate = 'Y'
                            $scope.doc.CanEditDocDueDate = 'Y'
                            $scope.doc.CanEditDocumentDate = 'Y'

                            if ($scope.objType === 'PQU' || $scope.objType === 'POR') {
                                $scope.doc.CanEditGrid = 'Y'
                                self.AddNewLineData()
                            }
                            else {
                                $scope.doc.CanEditGrid = 'N'
                            }
                        }

                        /*events received*/
                        $scope.$on('onGridKeyDown', function (e, args) {

                            let grid = args.grid,
                                gridData = grid.getData(),
                                gridItemData = grid.getDataItem(args.row),
                                field = grid.getColumns()[args.cell].field;

                            if ($scope.m.DocType === 'I') {

                                let itemCode = gridItemData['ItemCode']

                                let setGridItemData = function (d) {
                                    gridItemData['Dscription'] = d.ItemMaster.ItemName
                                    gridItemData['Quantity'] = 1
                                    gridItemData['UOM'] = d.ItemMaster.PurchasingUoM
                                    gridItemData['NumPerMsr'] = d.ItemMaster.ItemSalesPerUnit !== undefined ? d.ItemMaster.ItemSalesPerUnit : 0
                                    gridItemData['InvntryUOM'] = d.ItemMaster.InvntryUOM

                                    //warehouse
                                    let dfWarehouse = d.ItemMaster.ItemWarehouses.filter(q => q.IsDefault)[0]
                                    if (dfWarehouse) {
                                        $whs.GetWarehouseByID(dfWarehouse.WarehouseId).then(r2 => {
                                            if (r2.StatusCode === '0') {

                                                let whs = r2.DataResult
                                                gridItemData['WhsCode'] = whs.WhsCode

                                                if (d.ItemMaster.GLAccountSetupBy === 'W')
                                                    gridItemData['COGSAcct'] = whs.COGSAcct
                                                grid.invalidate();
                                            }
                                        })
                                    }

                                    //taxgroup
                                    let dfTaxGroup = d.PurTaxGroup
                                    if (dfTaxGroup) {
                                        gridItemData['VatGroup'] = dfTaxGroup.VatCode
                                        grid.invalidate();
                                    }

                                    //COGS
                                    let dfItemGroup = d.ItemGroup
                                    if (dfItemGroup) {
                                        if (d.ItemMaster.GLAccountSetupBy === 'G')
                                            gridItemData['COGSAcct'] = dfItemGroup.COGSAcct
                                        grid.invalidate();
                                    }

                                    self.AddNewLineData()
                                }

                                // get default values
                                if (field === 'ItemCode' || field === 'ItemName') {
                                    $itm.GetItemByCodeDocLookup(itemCode).then(res => {
                                        d = res.DataResult

                                        if (res.StatusCode === '0') {
                                            setGridItemData(d)
                                            $itmPackService.GetItemPackagingLookup(itemCode, "PUR").then(res4 => {
                                                if (res4.StatusCode === '0') {
                                                    let packDataList = res4.DataResult
                                                    $scope.uomList = []
                                                    packDataList.forEach(x => {
                                                        $scope.uomList.push({ "LookupID": x.LookupID, "LookupCode": x.LookupCode, "LookupName": x.LookupName })
                                                    })
                                                    $scope.g.gridColumns.filter(c => c.name === 'UOM')[0].dataSource = $scope.uomList
                                                }
                                            })
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
                                            gridItemData['WhsCode'] = whs.WhsCode
                                            grid.invalidate();
                                        }

                                        if (wResult.StatusCode === '-2') { // notfound
                                            $q.all([self.openModal('WHS', whsCode)]).then(function (modalRes) {
                                                whsData = modalRes[0].data
                                                gridItemData['WhsCode'] = whsData.WhsCode
                                                grid.invalidate();
                                            })
                                        }
                                    })
                                }

                                switch (field) {
                                    case 'WhsCode':
                                        break;

                                    case 'Price':
                                        self.ComputeLineTotal(args)
                                        break;
                                    case 'Quantity':
                                        self.ComputeLineTotal(args)
                                        break;

                                    case 'VatGroup':
                                        self.ComputeLineTotal(args)
                                        break;
                                }
                            }

                            if ($scope.m.DocType === 'S') {

                                switch (field) {
                                    case 'WhsCode':
                                        break;

                                    case 'Price':
                                        gridItemData['VatGroup'] = $scope.tg ? $scope.tg.VatCode : ""
                                        gridItemData['VatPercent'] = $scope.tg ? $scope.tg.Rate : ""
                                        self.ComputeLineTotal(args)
                                        break;
                                    case 'Quantity':
                                        //self.ComputeLineTotal(args)
                                        break;

                                    case 'VatGroup':
                                        self.ComputeLineTotal(args)
                                        break;
                                }

                            }
                        })

                        $scope.$on('onUpdateDocTotal', function (e, args) {

                            let grid = args.grid,
                                gridData = grid.getData(),
                                docTotalBeforeVat = 0,
                                vatSum = 0,
                                discountSum = 0;

                            gridData.forEach(x => {
                                docTotalBeforeVat += x.LineTotal;
                                vatSum += x.VatSum * 1;
                                discountSum += x.DiscountAmt;
                            })

                            $scope.m.TotalBeforeVat = docTotalBeforeVat;
                            $scope.m.DiscountSum = discountSum;
                            $scope.m.VatSum = vatSum;
                            $scope.m.DocTotal = docTotalBeforeVat * 1 + vatSum * 1
                        });

                        $scope.$on('onDocumentSavePostAction', function (e, args) {
                            console.log(args.data)
                        })

                        $scope.$on('onComputeLineTotal', function (e, args) {
                            self.ComputeLineTotal(args)
                        })

                        $scope.$on('onGridContextMenu', (e, args) => {
                            let
                                grid = args.grid,
                                gridData = grid.getData(),
                                cell = args.cell,
                                lineData = gridData[cell.row]


                            if (lineData['ItemCode'] !== '' || lineData['ItemCode'] !== undefined)
                                $itm.GetItemByCode(lineData['ItemCode']).then(res => {
                                    if (res.StatusCode === '0') {
                                        let itm = res.DataResult

                                        if (itm.ManageItemBy === 'S')
                                            $scope.isManageBySerial = true;
                                        else $scope.isManageBySerial = false;
                                    }
                                })
                        })

                        $scope.$on('onCtxMenuSerialNoClick', function (e, args) {

                            let grid = args.grid,
                                gridData = grid.getData(),
                                cell = args.cell,
                                lineData = gridData[cell.row],
                                qty = lineData['Quantity'],
                                serialNoList = [],
                                headerData = {};

                            if ($scope.objType === 'PDN') {
                                headerData.ItemCode = lineData['ItemCode']
                                headerData.ItemName = lineData['Dscription']
                                headerData.Quantity = lineData['Quantity']

                                if ($scope.recordMode === 'A') {
                                    for (var x = 0; x < qty; x++) {
                                        serialNoList.push({
                                            "$key": x,
                                            "ItemCode": lineData['ItemCode'],
                                            "ItemName": lineData['Dscription'],
                                            "Quantity": lineData['Quantity'],
                                            "WhsCode": lineData['WhsCode'],
                                            "InternalSerialNo": "",
                                            "SuppSerialNo": "",
                                            "InDate": $scope.m.DocDate,
                                            "BaseEntry": "",
                                            "BaseType": "",
                                            "BaseLine": lineData['LineNum'],
                                            "BaseDocNum": "",
                                            "CardCode": "",
                                            "CardName": "",
                                            "Status": "1",
                                            "Direction": "0",
                                        })
                                    }
                                }

                                if ($scope.recordMode === 'E') {
                                    serialNoList = $scope.s.SerialNumberList.filter(x => x.ItemCode === lineData['ItemCode'] && x.BaseLine === lineData['LineNum'])
                                }

                                $uibModal.open({
                                    templateUrl: `DocumentSerialNoInTmpl.html`,
                                    controller: 'SerialNoFormInCtrl',
                                    controllerAs: 'ctrl',
                                    windowClass: 'clsPopup',
                                    backdrop: 'static',
                                    keyboard: false,
                                    size: 'lg',
                                    resolve: {
                                        modalData: function () {
                                            return {
                                                'Title': 'Serial Numbers',
                                                'args': args,
                                                'HeaderData': headerData,
                                                'LineData': lineData,
                                                'SerialNoList': serialNoList
                                            };
                                        }
                                    } // data passed to the controller
                                }).result
                                    .then(function (d) {
                                        if (d) {
                                            if (d.length > 0) {
                                                d.forEach(x => {

                                                    if ($scope.recordMode === 'A') {
                                                        if ($scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode && y.ItemCode === d.ItemCode && y.$key === d.$key).length > 1) {
                                                            $scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode)[0] = x
                                                        }
                                                        else $scope.s.SerialNumberList.push(x)
                                                    }
                                                    if ($scope.recordMode === 'E') {
                                                        if ($scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode && y.ItemSerialNumberTransaction > 0).length > 1) {
                                                            $scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode)[0] = x
                                                        }
                                                        else $scope.s.SerialNumberList.push(x)
                                                    }
                                                })
                                            }
                                        }
                                    })
                            }

                            if ($scope.objType === 'PRD') {
                                $itemSerialTrn.GetAvailableSerialNo(lineData['ItemCode']).then(res => {
                                    if (res.StatusCode === '0') {
                                        headerData.ItemCode = lineData['ItemCode']
                                        headerData.ItemName = lineData['Dscription']
                                        headerData.Quantity = lineData['Quantity']
                                        d = res.DataResult

                                        if ($scope.recordMode === 'A') {
                                            if (d.length > 0) {
                                                d.forEach(x => serialNoList.push(x))
                                            }
                                        }

                                        if ($scope.recordMode === 'E') {
                                            /**/
                                        }

                                        $uibModal.open({
                                            templateUrl: `DocumentSerialNoOutTmpl.html`,
                                            controller: 'SerialNoFormOutCtrl',
                                            controllerAs: 'ctrl',
                                            windowClass: 'clsPopup',
                                            backdrop: 'static',
                                            keyboard: false,
                                            size: 'lg',
                                            resolve: {
                                                modalData: function () {
                                                    return {
                                                        'Title': 'Serial Numbers',
                                                        'args': args,
                                                        'HeaderData': headerData,
                                                        'LineData': lineData,
                                                        'SerialNoList': serialNoList
                                                    };
                                                }
                                            } // data passed to the controller
                                        }).result
                                            .then(function (d) {
                                                if (d) {
                                                    if (d.length > 0) {
                                                        d.forEach(x => {

                                                            if ($scope.recordMode === 'A') {
                                                                if ($scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode && y.ItemCode === d.ItemCode && y.$key === d.$key).length > 1) {
                                                                    $scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode)[0] = x
                                                                }
                                                                else $scope.s.SerialNumberList.push(x)
                                                            }
                                                            if ($scope.recordMode === 'E') {
                                                                if ($scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode && y.ItemSerialNumberTransaction > 0).length > 1) {
                                                                    $scope.s.SerialNumberList.filter(y => y.BaseLine === d.BaseLine && y.ItemCode === d.ItemCode)[0] = x
                                                                }
                                                                else $scope.s.SerialNumberList.push(x)
                                                            }
                                                        })
                                                    }
                                                }
                                            })

                                    }
                                })
                            }
                        })

                        $scope.onBtnSearch = function (e) {
                            alert('lol')
                        }

                        $scope.onBtnSaveClick = function (e) {
                            //console.log($scope.m.CardCode)
                            //$cs.saveChanges(() => {
                            //    console.log($scope.m.CardCode)
                            //})
                            let data = {
                                'objType': $scope.objType,
                                'docType': $scope.m.DocType, // Item or service
                                'docHeader': $scope.m,
                                'docDetail': $scope.g.gridData,
                                'docSerialNoDetail': $scope.s.SerialNumberList
                            }

                            $scope.$emit('onDocumentSaveAction', data)
                        }

                        $scope.onBtnCancelClick = function (e) {
                            $scope.$emit('onDocumentCancel', null)
                        }

                        $scope.onBtnFormSettings = function (e) {
                            $uibModal.open({
                                templateUrl: `FormSettingsTmpl.html`,
                                controller: 'FormSettingsCtrl',
                                controllerAs: 'ctrl',
                                windowClass: 'clsPopup',
                                backdrop: 'static',
                                keyboard: false,
                                resolve: {
                                    modalData: function () {
                                        return {
                                            'Title': 'Form Settings',
                                            //'GridColumns': $gridFactory.cols
                                            //'GridColumns': res[2]
                                            'GridColumns': self.ColData
                                        };
                                    }
                                } // data passed to the controller
                            }).result
                                .then(function (d) {

                                    if (d) {
                                        $scope.g.withUpdateCol = 'Y'
                                        UpdateColumns(d)
                                        //$scope.g.gridColumns = [];
                                        //d.filter(x => x.ShowColumn === 'Y').forEach(d => {
                                        //    $scope.g.gridColumns.push(UpdateColumnEditor(d))
                                        //})
                                    }

                                }, function () { })
                        }

                        $scope.onitem_0012_Change = function () {
                            $docFormGrid.GetColumns($scope.m.DocType).then((d) => {
                                if (d.StatusCode === '0') {
                                    var cols = d.DataResult
                                    $scope.g.withUpdateCol = 'Y'
                                    UpdateColumns(cols)
                                }
                            })
                        }

                        /*
                         * copy from/to
                         * */
                        self.CopyFromDoc = (baseType, docRes) => {
                            doc = docRes.DataResult
                            $scope.m.DocDueDate = $filter('date')(doc.DocDueDate, 'MM/dd/yyyy')
                            $scope.m.Remarks = `Based on ${doc.DocNum}`
                            if (docRes.StatusCode === '0') {
                                $scope.g.gridData = []
                                if (doc.Document_Lines.length > 0) {

                                    let docTotalBeforeVat = 0,
                                        vatSum = 0,
                                        discountSum = 0,
                                        lineCounter = 0

                                    doc.Document_Lines.forEach(x => {
                                        x.LineNum = ++lineCounter;
                                        if ($scope.m.DocType === 'I') {
                                            x.LineTotal = x.Price * x.Quantity
                                            x.GTotal = x.PriceAfVat * x.Quantity
                                        }

                                        if ($scope.m.DocType === 'S') {
                                            x.LineTotal = x.Price
                                            x.GTotal = x.PriceAfVat
                                            $scope.g.gridData.push(x)
                                        }

                                        x.BaseEntry = doc.DocID
                                        x.BaseType = baseType
                                        x.BaseLine = x.LineNum
                                        $scope.g.gridData.push(x)
                                        docTotalBeforeVat += x.LineTotal;
                                        vatSum += x.VatSum * 1;
                                        discountSum += x.DiscountAmt;
                                        $scope.m.TotalBeforeVat = docTotalBeforeVat;
                                        $scope.m.DiscountSum = discountSum;
                                        $scope.m.VatSum = vatSum;
                                        $scope.m.DocTotal = docTotalBeforeVat * 1 + vatSum * 1
                                    });
                                    self.AddNewLineData()
                                    $scope.g.withUpdateCol = 'Y'
                                }
                            }
                        }

                        $scope.onCopyFromQuotation = (e) => {
                            let baseType = 'PQU'
                            self.openModal(baseType, $scope.m.CardCode, 'lg').then(modalRes => {
                                //console.log(modalRes.data)
                                $pqu.GetPQUByID(modalRes.data.DocID).then(docRes => {
                                    self.CopyFromDoc(baseType, docRes)
                                })
                            })
                        }

                        $scope.onCopyFromOrder = (e) => {
                            let baseType = 'POR'
                            self.openModal(baseType, $scope.m.CardCode, 'lg').then(modalRes => {
                                //console.log(modalRes.data)
                                $por.GetPORByID(modalRes.data.DocID).then(docRes => {
                                    self.CopyFromDoc(baseType, docRes)
                                })
                            })
                        }

                        $scope.onCopyFromDelivery = (e) => {
                            let baseType = 'PDN'
                            self.openModal(baseType, $scope.m.CardCode, 'lg').then(modalRes => {
                                //console.log(modalRes.data)
                                $pdn.GetPDNByID(modalRes.data.DocID).then(docRes => {
                                    self.CopyFromDoc(baseType, docRes)
                                })
                            })
                        }

                        $scope.onCopyFromInvoice = (e) => {
                            let baseType = 'PIN'
                            self.openModal(baseType, $scope.m.CardCode, 'lg').then(modalRes => {
                                //console.log(modalRes.data)
                                $pin.GetPINByID(modalRes.data.DocID).then(docRes => {
                                    self.CopyFromDoc(baseType, docRes)
                                })
                            })
                        }

                        $scope.onCopyToDelivery = (e) => $window.open(`${document.baseUrlNoArea}AP/PurchaseDocument/PDN#!/BaseDoc/${$scope.objType}/${$scope.m.DocID}`, '_blank')

                        $scope.onCopyToReturnDelivery = (e) => $window.open(`${document.baseUrlNoArea}AP/PurchaseDocument/PRD#!/BaseDoc/${$scope.objType}/${$scope.m.DocID}`, '_blank')

                        $scope.onCopyToInvoice = (e) => $window.open(`${document.baseUrlNoArea}AP/PurchaseDocument/PIN#!/BaseDoc/${$scope.objType}/${$scope.m.DocID}`, '_blank')

                        $scope.onCopyToCreditNote = (e) => $window.open(`${document.baseUrlNoArea}AP/PurchaseDocument/PCR#!/BaseDoc/${$scope.objType}/${$scope.m.DocID}`, '_blank')

                        /*local functions*/
                        self.ComputeLineTotal = function (args) {
                            let grid = args.grid,
                                d = args.grid.getDataItem(args.row),
                                gridData = grid.getData(),
                                vatGroupCode = d.VatGroup;

                            $q.all([$tg.GetTaxGroupByCode(vatGroupCode)]).then(function (res) {

                                let vatGroup = res[0].DataResult,
                                    discount = d.Discount ? parseFloat(d.Discount) / 100 : 0,
                                    price = d.Price ? parseFloat(d.Price) : 0,
                                    computedPrice = discount > 0 ? price * (1 - (discount > 0 ? discount : 1)) : price,
                                    qty = d.Quantity ? parseFloat(d.Quantity) : 0,
                                    vatRate = vatGroup.Rate ? parseFloat(vatGroup.Rate) / 100 : 0;

                                d.PriceAfVat = computedPrice * (1 + vatRate)
                                d.LineTotal = computedPrice * qty
                                d.GTotal = d.Quantity * d.PriceAfVat
                                d.VatSum = computedPrice * vatRate * qty
                                d.VatPercent = vatGroup.Rate
                                d.DiscountAmt = discount > 0 ? price * discount : 0
                                grid.updateRow(args.row);
                                $scope.$emit('onUpdateDocTotal', args)
                            })
                        }

                        self.ComputeLineTotalService = function (args) {
                            let grid = args.grid,
                                d = args.grid.getDataItem(args.row),
                                gridData = grid.getData(),
                                vatGroupCode = d.VatGroup
                            $q.all([$tg.GetTaxGroupByCode(vatGroupCode)]).then(function (res) {
                                let vatGroup = res[0].DataResult,
                                    discount = d.Discount ? parseFloat(d.Discount) / 100 : 0,
                                    price = d.Price ? parseFloat(d.Price) : 0,
                                    computedPrice = discount > 0 ? price * (1 - (discount > 0 ? discount : 1)) : price,
                                    qty = d.Quantity ? parseFloat(d.Quantity) : 0,
                                    vatRate = vatGroup.Rate ? parseFloat(vatGroup.Rate) / 100 : 0;

                                d.PriceAfVat = computedPrice * (1 + vatRate)
                                d.LineTotal = computedPrice * qty
                                d.GTotal = d.Quantity * d.PriceAfVat
                                d.VatSum = computedPrice * vatRate * qty
                                d.VatPercent = vatGroup.Rate
                                d.DiscountAmt = discount > 0 ? price * discount : 0
                                grid.updateRow(args.row);
                                $scope.$emit('onUpdateDocTotal', args)
                            })
                        }

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

                        console.log($scope.g.gridOptions)

                    })
            }])
        .directive('cbDocTmpl', function ($http) {
            return {
                restrict: 'EA',
                controller: 'INVNTRYDocTemplateCtrl',
                scope: {
                    cLabel: '@cLabel',
                    objType: '@objType',
                    docData: '=docData'
                },
                link: function (scope, element, attrs) { },
                templateUrl: `DocumentFormTmpl.html`
            }
        })
        .controller('FormSettingsCtrl', ['$scope', '$uibModal', '$window', '$timeout', '$q', '$uibModalInstance', 'NgTableParams', 'modalData',
            function ($scope, $uibModal, $window, $timeout, $q, $uibModalInstance, $ngTable, modalData) {
                $scope.Title = modalData.Title
                //console.log(modalData)

                var loadTable = function () { $scope.tableParams = new $ngTable({}, { dataset: modalData.GridColumns }); }

                loadTable()

                $scope.onbtnSaveClick = function (s, e) {
                    $uibModalInstance.close(modalData.GridColumns);
                };

                $scope.onSelectClick = function (d, index) {
                    $scope.selectedRow = index;
                    $scope.selectedData = d;
                }

                $scope.onSelectDoubleClick = function (d) {
                    $uibModalInstance.close(d);
                }

                $scope.onbtnCancelClick = function (s, e) {
                    $uibModalInstance.dismiss('cancel');
                };

                $scope.onShowColumnClick = function (e, d) {
                    //$scope.tableParams.data.filter(x => x.id === d.id)[0].ShowColumn = d.ShowColumn

                    if (e.target.checked)
                        d.ShowColumn = 'Y'
                    else
                        d.ShowColumn = 'N'
                }
            }])
        .controller('SerialNoFormInCtrl', ['$scope', '$filter', '$uibModalInstance', 'NgTableParams', 'modalData',
            function ($scope, $filter, $uibModalInstance, $ngTable, modalData) {
                $scope.m = {}

                $scope.Title = modalData.Title

                $scope.m = modalData.HeaderData
                $scope.m.Quantity = $filter('number')($scope.m.Quantity, 2)

                var loadTable = function () { $scope.tableParams = new $ngTable({}, { dataset: modalData.SerialNoList }); }

                loadTable()

                $scope.onbtnSaveClick = function (s, e) {
                    $uibModalInstance.close(modalData.SerialNoList);
                };

                $scope.onGridSelectClick = function (d, index) {
                    $scope.selectedRow = index;
                    $scope.selectedData = d;
                }

                $scope.onGridSelectDoubleClick = function (d) {
                    $uibModalInstance.close(d);
                }

                $scope.onbtnCancelClick = function (s, e) {
                    $uibModalInstance.dismiss('cancel');
                };
            }])
        .controller('SerialNoFormOutCtrl', ['$scope', '$filter', '$uibModalInstance', 'NgTableParams', 'modalData',
            function ($scope, $filter, $uibModalInstance, $ngTable, modalData) {
                $scope.m = {}

                $scope.Title = modalData.Title

                $scope.m = modalData.HeaderData

                $scope.m.Quantity = $filter('number')($scope.m.Quantity, 2)

                $scope.SerialNoListOut = []

                var loadTable = function () { $scope.tableParamsSrc = new $ngTable({}, { dataset: modalData.SerialNoList }); }
                var loadTable2 = function () { $scope.tableParamsDst = new $ngTable({}, { dataset: $scope.SerialNoListOut }); }

                loadTable()
                loadTable2()

                $scope.onbtnSaveClick = function (s, e) {
                    $uibModalInstance.close($scope.tableParamsDst.data);
                };

                $scope.onLeftGridSelectClick = function (d, index) {
                    $scope.selectedRowLeft = index;
                    $scope.selectedDataLeft = d;
                    $scope.$emit('onLeftGridSelectClick', { args: { 'index': index, 'data': d } })
                }

                $scope.onLeftGridSelectDoubleClick = function (d) {
                    //$uibModalInstance.close(d);
                }

                $scope.onRightGridSelectClick = function (d, index) {
                    $scope.selectedRowRight = index;
                    $scope.selectedDataRight = d;
                    $scope.$emit('onRightGridSelectClick', { args: { 'index': index, 'data': d } })
                }

                $scope.onRightGridSelectDoubleClick = function (d) {
                    //$uibModalInstance.close(d);
                }

                $scope.onMoveRightAll = function (e) {
                    if ($scope.tableParamsSrc.data.length > 0) {
                        $scope.tableParamsSrc.data.forEach(x => {
                            $scope.tableParamsDst.data.push(x)
                        })

                        $scope.tableParamsSrc.data.splice(0)
                    }
                }

                $scope.onMoveRight = function (e) {
                    $scope.tableParamsSrc.data.splice($scope.selectedRowLeft, 1)
                    $scope.tableParamsDst.data.push($scope.selectedDataLeft)
                }

                $scope.onMoveLeftAll = function (e) {
                    if ($scope.tableParamsDst.data.length > 0) {
                        $scope.tableParamsDst.data.forEach(x => {
                            $scope.tableParamsSrc.data.push(x)
                        })

                        $scope.tableParamsDst.data.splice(0)
                    }
                }

                $scope.onMoveLeft = function (e) {
                    $scope.tableParamsDst.data.splice($scope.selectedRowRight, 1)
                    $scope.tableParamsSrc.data.push($scope.selectedDataRight)
                }

                $scope.onbtnCancelClick = function (s, e) {
                    $uibModalInstance.dismiss('cancel');
                };
            }])
        

}()); //docTemplateCtrl
