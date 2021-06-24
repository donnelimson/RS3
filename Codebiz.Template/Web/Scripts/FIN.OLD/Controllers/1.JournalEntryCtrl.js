/// <reference path="../Model/fin.model.js" />


(function () {

    var ctrl = function ($scope, $document, model, CoaService, JEService) {
        //CoaService.GetGLAccountPostableList
        var self = this;
        $scope.m = {};
        self.m = {}

        $scope.onDocDateTextChange = function () {

            $scope.m.DocDueDate = $scope.m.DocDate
            $scope.m.DocumentDate = $scope.m.DocDate
            //console.log($scope.m)


            self.dataSets.forEach(function (v, i, a) {
                v.RefDate = $scope.m.DocDate
                v.RefDate2 = $scope.m.DocDueDate
                v.RefDate3 = $scope.m.DocumentDate
            })

            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }

        angular.element("#txtDocDate").datepicker({
            orientation: "bottom right"
        })

        angular.element("#txtDocDueDate").datepicker({
            orientation: "bottom right"
        })

        angular.element("#txtDocumentDate").datepicker({
            orientation: "bottom right"
        })

        self.AutoCompleteEditor = function AutoCompleteEditor(args) {
            var $input;
            var defaultValue;
            var scope = this;
            var calendarOpen = false;
            this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER];
            this.init = function () {
                $input = $("<INPUT id='tags' class='editor-text' />");
                $input.appendTo(args.container);
                $input.focus().select();
                $input.autocomplete({
                    source: args.column.dataSource
                });
            };
            this.destroy = function () {
                $input.autocomplete("destroy");
                $input.remove();
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

        self.data = [];

        self.data2 = [];

        self.dataSets = [];

        (function () {
            self.dataSets = [];
            for (var x = 1; x < 2; x++) {
                self.dataSets.push({
                    id: x,
                    LineID: x,
                    ShortName: "",
                    AcctCode: "",
                    AcctName: "",
                    DebitCredit: "",
                    ContraAcct: "",
                    Debit: "0.00",
                    Credit: "0.00",
                    BaseAmt: "0.00",
                    TaxAmt: "0.00",
                    VatCode: "",
                    RefDate: "",
                    RefDate2: "",
                    RefDate3: "",
                    Ref1: "",
                    Ref2: "",
                    Ref3: "",
                    LineRemarks: "",
                });
            }
        }());

        self.options = {
            enableCellNavigation: true,
            enableColumnReorder: false,
            editable: true,
            enableAddRow: true,
            asyncEditorLoading: false,
            autoEdit: true,
            createFooterRow: true,
            showFooterRow: true,
            footerRowHeight: 28
        };

        self.deleteTask = function (data, row) {
            data.splice(row.row, 1);
        }

        $scope.onSaveJE = function () {

            var je = model.JournalEntry
            //var je_line  = model.JournalEntry_Line

            self.dataSets.forEach(function (d, i, a) {
                var je_line = {}
                if (d.AcctCode !== "" || d.ShortName !== "") {
                    je_line.ObjType = "JEN"
                    je_line.BaseEntry = ""
                    je_line.BaseNum = ""
                    je_line.BaseType = ""
                    je_line.TargetEntry = ""
                    je_line.TargetNum = ""
                    je_line.TargetType = ""
                    je_line.TransType = ""
                    je_line.GLAcctCode = d.AcctCode
                    je_line.LineID = i
                    je_line.VisID = i
                    je_line.ShortName = d.AcctCode
                    je_line.Debit = d.Debit
                    je_line.Credit = d.Credit
                    je_line.DebitSC = d.Debit
                    je_line.CreditSC = d.Credit
                    je_line.DebitFC = d.Debit
                    je_line.CreditFC = d.Credit
                    je_line.ContraAccount = ""
                    je_line.DebitCredit = d.Debit > 0 ? "D" : "C"
                    je_line.BaseAmt = "0"
                    je_line.VatAmt = "0"
                    je_line.RefDate = d.RefDate
                    je_line.RefDate2 = d.RefDate2
                    je_line.RefDate3 = d.RefDate3
                    je_line.Ref1 = d.Ref1
                    je_line.Ref2 = d.Ref2
                    je_line.Ref3 = d.Ref3
                    je_line.Project = ""
                    je_line.OcrCode = ""
                    je_line.OcrCode2 = ""
                    je_line.OcrCode3 = ""
                    je_line.OcrCode4 = ""
                    je_line.OcrCode5 = ""
                    je_line.LineRemarks = d.LineRemarks
                    je._AddLine(je_line)
                }
            })

            JEService.SaveOrUpdateJE(je).then(function (d) {
                je._ClearLine();

                self.dataSets.splice(0, self.dataSets.length)

                self.dataSets.push({
                    id: 1,
                    LineID: 1,
                    ShortName: "",
                    AcctCode: "",
                    AcctName: "",
                    DebitCredit: "",
                    ContraAcct: "",
                    Debit: "0.00",
                    Credit: "0.00",
                    BaseAmt: "0.00",
                    TaxAmt: "0.00",
                    VatCode: "",
                    RefDate: "",
                    RefDate2: "",
                    RefDate3: "",
                    Ref1: "",
                    Ref2: "",
                    Ref3: "",
                    LineRemarks: "",
                });

                $scope.$broadcast("onReloadGrid", "")
            })
        }
    }

    //angular.module('JE_APP', ['FIN.DATASERVICE', 'FIN.MODELS'])

    var app = angular.module('MetronicApp')
    app.controller('JournalEntryCtrl', ['$scope', '$document', 'JE.MODEL', 'CoaService', 'JEService', ctrl])
        .directive('myGrid', ['CoaService', function (CoaService) {
            return {
                restrict: 'A',
                scope: {
                    gridColumns: '=',
                    gridData: '=',
                    gridOptions: '=',
                    editRow: '&'
                },
                controller: 'JournalEntryCtrl',
                link: function ($scope, $element, $attr, $ctrl) {

                    CoaService.GetGLAccountPostableList().then(function (d) {

                        var acct_list = d;
                        var columns = [
                            {
                                id: "LineID", name: "#", field: "LineID", width: 30
                            },
                            {
                                id: "ShortName", name: "GL/BP Account Code", field: "ShortName", width: 150, editor: $ctrl.AutoCompleteEditor, dataSource: acct_list
                            },
                            {
                                id: "AccountName", name: "GL/BP Account Name", field: "AcctName", width: 300, editor: Slick.Editors.Text
                            },
                            {
                                id: "DebCred", name: "Line Type", field: "DebCred", width: 80
                            },
                            {
                                id: "Debit", name: "Debit", field: "Debit", width: 120, editor: Slick.Editors.Text
                            },
                            {
                                id: "Credit", name: "Credit", field: "Credit", width: 120, editor: Slick.Editors.Text
                            },
                            {
                                id: "Ref1", name: "Ref1", field: "Ref1", width: 120, editor: Slick.Editors.Text
                            },
                            {
                                id: "Ref2", name: "Ref2", field: "Ref2", width: 120, editor: Slick.Editors.Text
                            },
                            {
                                id: "Ref3", name: "Ref3", field: "Ref3", width: 120, editor: Slick.Editors.Text
                            },
                            {
                                id: "LineRemarks", name: "Remarks", field: "LineRemarks", width: 320, editor: Slick.Editors.Text
                            },
                            {
                                id: "VatCode", name: "Tax Code", field: "VatCode", width: 120
                            },
                            {
                                id: "DocDate", name: "Posting Date", field: "RefDate", width: 120, editor: Slick.Editors.Date
                            },
                            {
                                id: "DocDueDate", name: "Due Date", field: "RefDate2", width: 120, editor: Slick.Editors.Date
                            },
                            {
                                id: "DocumentDate", name: "Doc. Date", field: "RefDate3", width: 120, editor: Slick.Editors.Date
                            }
                        ];

                        var grid;
                        var dv;
                        //var acct_list = [];
                        $scope.$watchCollection('gridData', function (newVal, oldVal) {
                            if (newVal && grid) {
                                grid.invalidate();
                            }
                        });

                        function UpdateTotal(cell, grid) {

                            var columnId = grid.getColumns()[cell].id;

                            var total = 0;
                            var i = $scope.gridData.length;
                            while (i--) {
                                total += (parseFloat($scope.gridData[i][columnId], 10) || 0);
                            }
                            var columnElement = grid.getFooterRowColumn(columnId);
                            if (grid.getColumns()[cell].field === 'Debit' || grid.getColumns()[cell].field === 'Credit') {
                                $(columnElement).html('<b style="float:right">' + total.toFixed(4) + '</b>');
                            }
                            else {
                                $(columnElement).html("&nbsp;");

                            }
                        }

                        function UpdateAllTotals(grid) {
                            var columnIdx = grid.getColumns().length;
                            while (columnIdx--) {
                                UpdateTotal(columnIdx, grid);
                            }
                        }

                        dv = new Slick.Data.DataView();
                        grid = new Slick.Grid($element[0], $scope.gridData, columns, $scope.gridOptions);
                        UpdateAllTotals(grid);

                        grid.onDblClick.subscribe(function (e, args) {
                            $scope.editRow({ row: args });
                            grid.invalidate();
                        });

                        grid.onKeyDown.subscribe(function (e, args) {
                            if (e.key === 'Tab' && grid.getColumns()[args.cell].field === 'ShortName') {
                                if (!grid.getEditorLock().commitCurrentEdit()) {
                                    return;
                                }
                                var d = args.grid.getDataItem(args.row)

                                CoaService.GetGLAccountByAcctCode(d.ShortName).then(function (d) {
                                    $scope.gridData[args.row].AcctCode = d.AcctCode
                                    $scope.gridData[args.row].AcctName = d.AcctName
                                    $scope.gridData[args.row].RefDate = $scope.$parent.m.DocDate
                                    $scope.gridData[args.row].RefDate2 = $scope.$parent.m.DocDueDate
                                    $scope.gridData[args.row].RefDate3 = $scope.$parent.m.DocumentDate
                                    //console.log($scope)
                                    grid.updateRow(args.row);



                                    //console.log($scope.gridData)
                                    // e.stopPropagation();

                                    $scope.gridData.push({
                                        
                                        LineID: $scope.gridData.length + 1,
                                        ShortName: "",
                                        AcctCode: "",
                                        AcctName: "",
                                        DebitCredit: "",
                                        ContraAcct: "",
                                        Debit: "0.00",
                                        Credit: "0.00",
                                        BaseAmt: "0.00",
                                        TaxAmt: "0.oo",
                                        VatCode: "",
                                        RefDate: "",
                                        RefDate2: "",
                                        RefDate3: "",
                                        Ref1: "",
                                        Ref2: "",
                                        Ref3: "",
                                        LineRemarks: "",
                                    })
                                });
                                grid.render();
                            }
                            if (e.key === 'Tab' && (grid.getColumns()[args.cell].field === 'Debit' || grid.getColumns()[args.cell].field === 'Credit')) {

                                if (!grid.getEditorLock().commitCurrentEdit()) {
                                    return;
                                }
                                var dd = args.grid.getDataItem(args.row)
                                grid.updateRow(args.row);
                                UpdateTotal(args.cell, grid)
                                grid.render();
                            }

                        });

                        grid.onCellChange.subscribe(function (e, args) {
                            //UpdateTotal(args.cell, args.grid);
                        });

                        $scope.$on("onReloadGrid", function (e, v) {

                            //$scope.gridData.splice(0, $scope.gridData.length)
                            //$scope.gridData.push({
                            //    id : $scope.gridData.length + 1,
                            //    LineID: $scope.gridData.length + 1,
                            //    ShortName: "",
                            //    AcctCode: "",
                            //    AcctName: "",
                            //    DebitCredit: "",
                            //    ContraAcct: "",
                            //    Debit: "0.00",
                            //    Credit: "0.00",
                            //    BaseAmt: "0.00",
                            //    TaxAmt: "0.00",
                            //    VatCode: "",
                            //    RefDate: "",
                            //    RefDate2: "",
                            //    RefDate3: "",
                            //    Ref1: "",
                            //    Ref2: "",
                            //    Ref3: "",
                            //    LineRemarks: "",
                            //})

                            //console.log($scope.gridData)

                            dv.beginUpdate();
                            dv.setItems($scope.gridData);
                            dv.endUpdate();

                            dv.refresh();
                            grid.invalidate();

                            //grid.updateRowCount();
                            //grid.render();

                            //grid.invalidate();
                            alert('Save Sucessfuly')
                        })
                    })
                }
            }
        }]); //end Directive

}());