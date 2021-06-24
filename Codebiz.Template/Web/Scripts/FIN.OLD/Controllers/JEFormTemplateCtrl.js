(function () {

    let doc = angular.module('cb.je.doctemplate', [
        'FIN.DATASERVICE',
        'BP.SETUP.SERVICE',
        'SL.DATASERVICE',
    ])

    doc.controller('JEFormTemplateCtrl', [
        '$scope',
        '$rootScope',
        '$filter',
        '$uibModal',
        '$window',
        '$timeout',
        '$q',
        'CommonService',
        'CoaService',
        'BusinessPartnerService',
        'TaxGroupService',
        'jen.formgrid.service',
        'cfl.bp.service',
        'cfl.invntry.service',
        'cfl.fin.service',
        'SelectListService',
        'jen.docnum.service',
        function (
            $scope,
            $rootScope,
            $filter,
            $uibModal,
            $window,
            $timeout,
            $q,
            $cs,
            $coa,
            $bp,
            $tgService,
            $docFormGrid,
            $cflBPService,
            cflInvnrtyService,
            cflFinService,
            $selectService,
            $docNum

        ) {

            var self = this;

            self.openModal = function (linkType, linkSubtype, searchText, modal_size) {

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
                                'CardType': `${linkSubtype}`,
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

                    let inputType = 'text'


                    switch (args.column.field) {
                        case 'GLAccountCode':
                            inputType = 'text'
                            break;
                        case 'ShortName':
                            inputType = 'text'
                            break;
                        case 'Debit':
                            inputType = 'number'
                            break;
                        case 'Credit':
                            inputType = 'number'
                            break;
                    }


                    $inputElement = $(`<div class="input-icon right">
                                            <i id="OBJ1002" name="OBJ1002" class="fa fa-search"  style="color:#32D5C2 !important; margin-top:1px !important;"></i>
                                            <input id="GOBJ1001" type="${inputType}" name="GOBJ1001" class="editor-text" value="" autocomplete="off" />
                                            </div>`);

                    $inputElement.appendTo(args.container);
                    $input = $(`input[name="GOBJ1001"]`).select()
                    $input.focus().select();


                    let linkType = '',
                        linkSubType = ''

                    if (args.column.field === "ItemCode")
                        linkType = 'ITM'

                    if (args.column.field === "WhsCode")
                        linkType = 'WHS'

                    if (args.column.field === "AcctCode" || args.column.field === "GLAcctCode" || args.column.field === "ShortName") {
                        linkType = 'ACT'
                    }


                    if (args.column.field === "AcctName" || args.column.field === "GLAcctName")
                        linkType = 'ACT'

                    if (args.column.field === "COGSAcct")
                        linkType = 'ACT'


                    $("i[name='OBJ1002']").on('click', function (e) {
                        //console.log(e)

                        if (e.ctrlKey) {
                            linkType = 'CRD'
                            linkSubType = 'A'
                        }

                        else
                            linkType = 'ACT'

                        $q.all([self.openModal(linkType, linkSubType, '', 'md')]).then(function (d) {
                            if (d) {
                                if (linkType === 'ITM') {
                                    $input.val(d[0].data.ItemCode)
                                    newValue = d[0].data.ItemCode
                                    selectedValue = d[0].data
                                    selectedValue['linkType'] = linkType
                                    selectedValue['linkSubType'] = linkSubType
                                }

                                if (linkType === 'CRD') {
                                    $input.val(d[0].data.CardCode)
                                    newValue = d[0].data.CardCode
                                    selectedValue = d[0].data
                                    selectedValue['linkType'] = linkType
                                    selectedValue['linkSubType'] = linkSubType
                                }

                                if (linkType === 'WHS') {
                                    $input.val(d[0].data.WhsCode)
                                    newValue = d[0].data.WhsCode
                                    selectedValue = d[0].data
                                    selectedValue['linkType'] = linkType
                                    selectedValue['linkSubType'] = linkSubType
                                }

                                if (linkType === 'ACT') {
                                    $input.val(d[0].data.AcctCode)
                                    newValue = d[0].data.AcctCode
                                    selectedValue = d[0].data
                                    selectedValue['linkType'] = linkType
                                    selectedValue['linkSubType'] = linkSubType
                                }
                            }
                        })
                    })

                    $("input[name='GOBJ1001']").on('keydown', (e) => {
                        //console.log($("input[name='GOBJ1001']").val())
                        //e.preventDefault()
                        console.log(e)
                        if (!e.ctrlKey && e.key === '/') {
                            linkType = 'ACT'
                        }
                        else if (e.ctrlKey && e.key === '/') {
                            linkType = 'CRD'
                            linkSubtype = 'A'
                        }

                        if (e.key === '/') {
                            $("input[name='GOBJ1001']").val('')
                            $q.all([self.openModal(linkType, linkSubtype, '', 'md')]).then(function (d) {
                                if (d) {
                                    if (linkType === 'ITM') {
                                        $input.val(d[0].data.ItemCode)
                                        newValue = d[0].data.ItemCode
                                        selectedValue = d[0].data
                                        selectedValue['linkType'] = linkType
                                        selectedValue['linkSubType'] = linkSubType
                                    }

                                    if (linkType === 'CRD') {
                                        $input.val(d[0].data.CardCode)
                                        newValue = d[0].data.CardCode
                                        selectedValue = d[0].data
                                        selectedValue['linkType'] = linkType
                                        selectedValue['linkSubType'] = linkSubType
                                    }

                                    if (linkType === 'WHS') {
                                        $input.val(d[0].data.WhsCode)
                                        newValue = d[0].data.WhsCode
                                        selectedValue = d[0].data
                                        selectedValue['linkType'] = linkType
                                        selectedValue['linkSubType'] = linkSubType
                                    }

                                    if (linkType === 'ACT') {
                                        $input.val(d[0].data.AcctCode)
                                        newValue = d[0].data.AcctCode
                                        selectedValue = d[0].data
                                        selectedValue['linkType'] = linkType
                                        selectedValue['linkSubType'] = linkSubType
                                    }
                                }
                            })
                        }
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
                        if (selectedValue) {
                            item['Dscription'] = selectedValue.ItemName

                        }

                    if (args.column.field === 'ShortName')
                        if (selectedValue) {
                            if (selectedValue.linkType === 'ACT') {
                                item['GLAcctName'] = selectedValue.AcctName
                                item['linkType'] = selectedValue.linkType
                                item['linkSubType'] = selectedValue.linkSubType
                            }

                            if (selectedValue.linkType === 'CRD') {
                                item['GLAcctName'] = selectedValue.CardName
                                item['linkType'] = selectedValue.linkType
                                item['linkSubType'] = selectedValue.linkSubType
                            }
                        }

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
                                newOption = new Option(`${x['LookupCode']} - ${x['LookupName']}`, x['LookupCode']);
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

            self.UpdateTotals = function (cell, grid) {

                var colField = grid.getColumns()[cell].field,
                    colID = grid.getColumns()[cell].id,
                    gridData = grid.getData()

                var total = 0;
                var i = gridData.length;
                while (i--) {
                    total += (parseFloat(gridData[i][colField], 10) || 0);
                }
                var columnElement = grid.getFooterRowColumn(colID);

                if (grid.getColumns()[cell].field === 'Debit' || grid.getColumns()[cell].field === 'Credit') {

                    let totalAmt = $filter('number')(total, '2')

                    $(columnElement).html(`<b style="float:right">${totalAmt}</b>`);
                }
                else {
                    $(columnElement).html("&nbsp;");
                }
            }

            self.UpdateColumnTotal = function (col, grid) {

                var colField = col
                    colID = grid.getColumns().filter(x=>x.field === col)[0].id,
                    gridData = grid.getData()

                var total = 0;
                var i = gridData.length;
                while (i--) {
                    total += (parseFloat(gridData[i][colField], 10) || 0);
                }
                var columnElement = grid.getFooterRowColumn(colID);

                if (col === 'Debit' || col === 'Credit') {

                    let totalAmt = $filter('number')(total, '2')

                    $(columnElement).html(`<b style="float:right">${totalAmt}</b>`);
                }
                else {
                    $(columnElement).html("&nbsp;");
                }
            }

            $q.all([
                $selectService.GetTaxGroupLookupByCategorySL('A').then((d) => { return d; }),
                $scope.docData.model,
                $docFormGrid.GetColumns('JEN','I').then((d) => { return d.DataResult }),
                $docNum.GetDocNum($scope.objType).then(d => { return d.StatusCode === '0' ? d.DataResult : null }),
                $scope.docData.DocAction,
            ]).then(
                function (res) {

                    self.ColData = res[2]
                    self.Action = res[4].Action

                    $scope.g = {}
                    $scope.g.gridOptions = {}
                    $scope.m = {}
                    $scope.doc = {}
                    $scope.doc.CanEditCardCode = self.Action === 'EDIT' ? 'Y' : 'N'
                    $scope.doc.CanEditDocDate = self.Action === 'EDIT' ? 'Y' : 'N'
                    $scope.doc.CanEditDocDueDate = self.Action === 'EDIT' ? 'Y' : 'N'
                    $scope.doc.CanEditDocumentDate = self.Action === 'EDIT' ? 'Y' : 'N'
                    //$scope.doc.CanEditGrid = self.Action === 'EDIT' ? 'Y' : 'N'
                    $scope.doc.CanEditDoc = self.Action === 'EDIT' || self.Action === 'ADD' ? 'Y' : 'N'



                    self.SetGridEditStatus = function (c) {
                        if (c) {
                            $scope.g.gridOptions.editable = true
                            $scope.g.withUpdateCol = 'Y'
                            $scope.canAddEdit = true
                        }
                        else {
                            $scope.g.gridOptions.editable = false
                            $scope.g.withUpdateCol = 'Y'
                            $scope.canAddEdit = false
                        }
                    }

                    //$scope.vatGroupList = res[0].DataResult

                    $scope.m = res[1]

                    //$scope.g.gridRecordOption = {
                    //    "CanAdd": "N",
                    //    "CanEdit": "N",
                    //    "CanDelete": "N"
                    //};

                    $scope.m.DocnumType = "A"

                    $scope.g.gridData = []

                    self.AddNewLineData = function () {
                        $scope.g.gridData.push
                            ({
                                "ObjType": "JEN",
                                "BaseEntry": "",
                                "BaseNum": "",
                                "BaseType": "",
                                "TargetEntry": "",
                                "TargetNum": "",
                                "TargetType": "",
                                "TransType": "",
                                "GLAcctCode": "",
                                "LineID": $scope.g.gridData.length + 1,
                                "VisID": "",
                                "ShortName": "",
                                "Debit": "",
                                "Credit": "",
                                "DebitSC": "",
                                "CreditSC": "",
                                "DebitFC": "",
                                "CreditFC": "",
                                "ContraAccount": "",
                                "DebitCredit": "",
                                "BaseAmt": "",
                                "VatAmt": "",
                                "RefDate": "",
                                "RefDate2": "",
                                "RefDate3": "",
                                "Ref1": "",
                                "Ref2": "",
                                "Ref3": "",
                                "Project": "",
                                "OcrCode": "",
                                "OcrCode2": "",
                                "OcrCode3": "",
                                "OcrCode4": "",
                                "OcrCode5": "",
                                "LineRemarks": ""
                            })
                    }
                    // set BP 
                    if ($scope.m.TransID > 0) {
                        $scope.recordMode = 'E'
                        $scope.s = {}
                        $scope.s.SerialNumberList = []
                        $scope.m.RefDate = $filter('date')($scope.m.RefDate, 'MM/dd/yyyy');
                        $scope.m.RefDate2 = $filter('date')($scope.m.RefDate2, 'MM/dd/yyyy');
                        $scope.m.RefDate3 = $filter('date')($scope.m.RefDate3, 'MM/dd/yyyy');
                        $scope.DocSeriesList = res[3].DocSeriesList

                        if ($scope.m.JournalEntry_Lines.length > 0) {
                            let lineCounter = 0

                            $scope.m.JournalEntry_Lines.forEach(x => {
                                x.LineNum = ++lineCounter;
                                $scope.g.gridData.push(x)


                                if (x.GLAcctCode === x.ShortName) {
                                    $coa.GetGLAccountByAcctCode(x.ShortName).then((res) => {
                                        if (res.StatusCode === '0') {
                                            $scope.g.gridData.filter(y => y.GLAcctCode === x.GLAcctCode)[0]['GLAcctName'] = res.DataResult.AcctName
                                            $scope.g.withUpdateCol = 'Y'
                                        }
                                    })
                                }
                                else {
                                    $bp.GetBusinessPartnerByCode(x.ShortName).then((res) => {
                                        if (res.StatusCode === '0') {
                                            $scope.g.gridData.filter(y => y.GLAcctCode === x.GLAcctCode)[0]['GLAcctName'] = res.DataResult.CardName
                                            $scope.g.withUpdateCol = 'Y'
                                        }
                                            
                                    })
                                }
                            })
                        }

                        for (var x = 0; x < 50 - $scope.m.JournalEntry_Lines.length; x++) {
                            self.AddNewLineData()
                        }

                        self.SetGridEditStatus(true)
                    }

                    else {
                        $scope.recordMode = 'A'
                        $scope.s = {}
                        $scope.s.SerialNumberList = []
                        $scope.m.DocNum = res[3].DocNextNum
                        $scope.DocSeriesList = res[3].DocSeriesList
                        $scope.m.Series = res[3].DocSeriesList.filter(x => x.IsDefault)[0].LookupCode
                        //$scope.canAddEdit = true

                        $scope.doc.CanEditGrid = 'Y'
                        for (var x = 0; x < 50; x++) {
                            self.AddNewLineData()
                        }
                        self.SetGridEditStatus(true)
                    }

                    let UpdateColumnEditor = function (d) {

                        if ($scope.recordMode === 'A' && self.Action === 'ADD') {
                            $scope.canAddEdit = true
                        }


                        if (d.originalname === 'LineID') {
                            d.headerCssClass = 'text-center'
                            d.cssClass = 'text-center'
                        }

                        if (d.originalname === 'LineTotal' || d.originalname === 'LineTotalFC'
                            || d.originalname === 'Debit' || d.originalname === 'DebitFC' || d.originalname === 'DebitSC'
                            || d.originalname === 'Credit' || d.originalname === 'CreditFC' || d.originalname === 'CreditSC'
                        ) {
                            d.cssClass = 'text-right'
                            d.editor = Slick.Editors.Float
                            d.headerCssClass = 'text-center'
                        }

                        if (d.originalname === 'VatGroup') {
                            if ($scope.canAddEdit) {
                                d.editor = self.SelectListEditor
                                d.dataSource = res[0].DataResult
                            }
                            d.headerCssClass = 'text-center'
                            d.width = 150
                        }

                        if (d.originalname === 'GLAcctCode') {
                            d.headerCssClass = 'text-center'
                        }

                        if (d.originalname === 'ShortName') {
                            if ($scope.canAddEdit) {
                                d.editor = self.LinkTextBoxEditor
                            }
                            d.headerCssClass = 'text-center'
                        }

                        if (d.originalname === 'GLAcctName') {
                            d.headerCssClass = 'text-center'
                        }

                        if (d.originalname === 'LineRemarks') {
                            if ($scope.canAddEdit) {
                                d.editor = Slick.Editors.Text
                            }
                            d.headerCssClass = 'text-center'
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

                    /*events received*/
                    $scope.$on('onGridKeyDown', function (e, args) {

                        let grid = args.args.grid,
                            gridData = grid.getData(),
                            gridItemData = grid.getDataItem(args.args.row),
                            field = grid.getColumns()[args.args.cell].field;

                        //console.log(args.keyEvent)


                        let shortName = gridItemData['ShortName'],
                            linkType = gridItemData['linkType']


                        switch (field) {
                            case 'ShortName':
                                if (linkType === 'ACT') {
                                    $coa.GetGLAccountByAcctCode(shortName).then((res) => {
                                        if (res.StatusCode === '0') {
                                            let d = res.DataResult
                                            gridItemData['GLAcctCode'] = d.AcctCode
                                            gridItemData['GLAcctName'] = d.AcctName
                                            grid.invalidate()
                                        }
                                    })
                                }
                                if (linkType === 'CRD') {
                                    $bp.GetBusinessPartnerByCode(shortName).then((res) => {
                                        if (res.StatusCode === '0') {
                                            let d = res.DataResult
                                            gridItemData['GLAcctCode'] = d.CtlDebCredPayAcct
                                            gridItemData['GLAcctName'] = d.CardName
                                            grid.invalidate()
                                        }
                                    })
                                }
                                break;

                            case 'GLAcctCode':
                                break;

                            case 'GLAcctName':
                                break;

                            case 'Debit':
                                self.UpdateTotals(args.args.cell, grid)
                                break;

                            case 'Credit':
                                self.UpdateTotals(args.args.cell, grid)
                                break;

                            case 'VatGroup':

                                $tgService.GetTaxGroupByCode(gridItemData['VatGroup']).then(res => {
                                    if (res.StatusCode === '0') {
                                        let d = res.DataResult
                                        $coa.GetGLAccountByAcctCode(d.Account).then((res) => {
                                            if (res.StatusCode === '0') {
                                                let gl = res.DataResult
                                                gridItemData['GLAcctCode'] = gl.AcctCode
                                                gridItemData['ShortName'] = gl.AcctCode
                                                gridItemData['GLAcctName'] = gl.AcctName
                                                grid.invalidate()
                                            }
                                        })
                                    }
                                })
                                break;
                        }
                    })

                    $scope.$on('onUpdateDocTotal', function (e, args) {

                        let grid = args.grid,
                            gridData = grid.getData(),
                            docTotalBeforeVat = 0,
                            vatSum = 0,
                            discountSum = 0;
                    });

                    $scope.$on('onDocumentSavePostAction', function (e, args) {
                        console.log(args.data)
                    })

                    $scope.$on('onGridContextMenu', (e, args) => {
                        let
                            grid = args.grid,
                            gridData = grid.getData(),
                            cell = args.cell,
                            lineData = gridData[cell.row]
                    })

                    $scope.$on('onLoadGrid', (e, args) => {
                        let grid = args
                        self.UpdateColumnTotal('Debit', grid)
                        self.UpdateColumnTotal('Credit', grid)
                    })

                    $scope.onBtnSearch = function (e) {
                        alert('lol')
                    }

                    $scope.onBtnSaveClick = function (e) {
                        let data = {
                            'objType': $scope.objType,
                            'docType': $scope.m.DocType, // Item or service
                            'docHeader': $scope.m,
                            'docDetail': $scope.g.gridData,
                            'docSerialNoDetail': $scope.s.SerialNumberList
                        }
                        $scope.$emit('onJEDocSaveAction', data)
                    }

                    $scope.onBtnCancelClick = function (e) {
                        $scope.$emit('onJEDocCancelAction', $scope.m)
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
                        showFooterRow: true,
                        footerRowHeight: 28,
                        enableAutoSizeColumns: true,
                    };
                })
        }
    ])
        .directive('cbJeTmpl', function ($http) {
            return {
                restrict: 'EA',
                controller: 'JEFormTemplateCtrl',
                scope: {
                    cLabel: '@cLabel',
                    objType: '@objType',
                    docData: '=docData'
                },
                link: function (scope, element, attrs) { },
                templateUrl: `JEFormTmpl.html`
            }
        })


}())