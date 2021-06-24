(function () {
    let app = angular.module('cb.doc.FormGrid', ['FIN.DATASERVICE'])

    app
        .controller('docFormGridCtrl', ['$scope', '$q', 'TaxGroupService', function ($scope, $q, $taxgroupService) {

            var self = this;

            $scope.m = {};

            self.docLineID = $scope.$id

            self.deleteTask = function (data, row) {
                data.splice(row.row, 1);
            }

            self.ComputeLineTotal = function (args) {
                let grid = args.grid;
                let d = args.grid.getDataItem(args.row)
                let gridData = grid.getData()
                let vatGroupCode = d.VatGroup

                $q.all([$taxgroupService.GetTaxGroupByCode(vatGroupCode)]).then(function (res) {

                    let vatGroup = res[0].DataResult;

                    let discount = d.Discount ? parseFloat(d.Discount) / 100 : 0;

                    let price = d.Price ? parseFloat(d.Price) : 0;

                    let computedPrice = discount > 0 ? price * (1 - (discount > 0 ? discount : 1)) : price;

                    let qty = d.Quantity ? parseFloat(d.Quantity) : 0;

                    let vatRate = vatGroup.Rate ? parseFloat(vatGroup.Rate) / 100 : 0;

                    d.PriceAfVat = computedPrice * (1 + vatRate)

                    d.LineTotal = computedPrice * qty

                    d.GTotal = d.Quantity * d.PriceAfVat

                    d.VatSum = computedPrice * vatRate

                    d.VatPercent = vatGroup.Rate

                    d.DiscountAmt = discount > 0 ? price * discount : 0

                    grid.updateRow(args.row);

                    $scope.$emit('onUpdateDocTotal', args)
                })
            }

            self.ComputeGrossTotal = function (args) {
                var d = args.grid.getDataItem(args.row)
                grid.updateRow(args.row);
            }

            self.ComputeGrossPrice = function (args) {
                var d = args.grid.getDataItem(args.row)
                grid.updateRow(args.row);
            }

            self.GetDefaultVatCode = function (vatCode) {
                return $taxgroupService.GetTaxGroupByCode(vatGroupCode).then(function (res) {
                    return res
                })
            }

            self.ComputeGridTotals = function (args) {

                let grid = args.grid

                let gridData = grid.getData()

                gridData.forEach(d => {

                    $q.all([$taxgroupService.GetTaxGroupByCode(d.VatGroup)]).then(function (res) {

                        let vatGroup = res[0].DataResult;

                        let discount = d.Discount ? parseFloat(d.Discount) / 100 : 0;

                        let price = d.Price ? parseFloat(d.Price) : 0;

                        let computedPrice = discount > 0 ? price * (1 - (discount > 0 ? discount : 1)) : price;

                        let qty = d.Quantity ? parseFloat(d.Quantity) : 0;

                        let vatRate = vatGroup.Rate ? parseFloat(vatGroup.Rate) / 100 : 0;

                        d.PriceAfVat = computedPrice * (1 + vatRate)

                        d.LineTotal = computedPrice * qty

                        d.GTotal = d.Quantity * d.PriceAfVat

                        d.VatSum = computedPrice * vatRate

                        d.VatPercent = vatGroup.Rate

                        d.DiscountAmt = discount > 0 ? price * discount : 0

                        grid.invalidate()

                        $scope.$emit('onUpdateDocTotal', args)
                    })
                })
            }

            $("#gridContextMenu").click(function (e) {

                e.preventDefault()

                var data = $(this).data("data");

                let grid = data.grid

                let args = data.args

                let gridData = grid.getData()

                let cell = data.cell

                if (!$(e.target).is("a")) {
                    return;
                }
                if (!grid.getEditorLock().commitCurrentEdit()) {
                    return;
                }

                let menuitemData = $(e.target).attr("menuitem-data");

                //console.log(cell)

                let updateGrid = function () {
                    counter = 0;
                    gridData.map(x => x.LineNum = ++counter)
                    grid.invalidate()
                }

                switch (menuitemData) {
                    case 'CUT':
                        break;
                    case 'COPY':
                        break;
                    case 'PASTE':
                        break;
                    case 'DELETE':
                        break;
                    case 'ADDROW':
                        break;
                    case 'DELROW':

                        gridData.splice(cell.row, 1)
                        updateGrid()
                        self.ComputeGridTotals(args)
                        break;
                    case 'DUPROW':

                        let dupData = JSON.parse(JSON.stringify(gridData.slice(cell.row)[0]))

                        dupData.$id = self.getNextLineID()
                        dupData.DocID = 0;
                        dupData.DocLineID = 0;

                        gridData.splice(gridData.length - 1, 0, dupData)
                        updateGrid()
                        self.ComputeGridTotals(args)
                        break;
                    case 'SAVEDRAFT':
                        $scope.$emit('onCtxMenuSaveDraftClick', data)
                        break;

                    case 'SERIALNO':
                        $scope.$emit('onCtxMenuSerialNoClick', data)
                        break;

                }
            });

            self.getNextLineID = function () {
                return self.docLineID++;
            }

            /*events*/
            self.onKeyDownGrid = function (e, args) {
                let grid = args.grid;
                if (e.key === 'Tab' || e.key === 'Enter') {
                    if (!grid.getEditorLock().commitCurrentEdit()) {
                        return;
                    }

                    let field = grid.getColumns()[args.cell].field

                    switch (field) {
                        case 'ItemCode':
                            var d = args.grid.getDataItem(args.row)


                            if (d[field] !== '' && (e.key === 'Tab' || e.key === 'Enter')) {

                                //let lineNum = $scope.gridData.length + 1

                                //$scope.gridData.push
                                //    ({
                                //        $id: self.getNextLineID(),
                                //        "DocNum": "",
                                //        "LineNum": lineNum,
                                //        "VisNum": "",
                                //        "LineType": "",
                                //        "Text": "",
                                //        "ObjType": "",
                                //        "BaseEntry": "",
                                //        "BaseType": "",
                                //        "BaseLine": "",
                                //        "BaseDocNum": "",
                                //        "BaseDocRef": "",
                                //        "TargetEntry": "",
                                //        "LineStatus": "",
                                //        "TargetType": "",
                                //        "TargetLine": "",
                                //        "TargetDocRef": "",
                                //        "Currency": "",
                                //        "CardCode": "",
                                //        "CardName": "",
                                //        "DocDate": "",
                                //        "ItemCode": "",
                                //        "Dscription": "",
                                //        "Dscription2": "",
                                //        "WhsCode": "",
                                //        "Quantity": "1",
                                //        "OpenQty": "",
                                //        "QtyPerPacUOM": "",
                                //        "BaseQty": "",
                                //        "BaseOpenQty": "",
                                //        "QtyToShip": "",
                                //        "DeliveredQty": "",
                                //        "OrderedQty": "",
                                //        "NumPerMsr": "",
                                //        "ShipDate": "",
                                //        "ShipToCode": "",
                                //        "ShipToDesc": "",
                                //        "UOM": "",
                                //        "TotalInvntryQty": "",
                                //        "StockPrice": "",
                                //        "StockValue": "",
                                //        "INMPRice": "",
                                //        "PickQty": "",
                                //        "PickStatus": "",
                                //        "InvntryUOM": "",
                                //        "ItemCost": "",
                                //        "Price": "",
                                //        "PriceAfVat": "",
                                //        "Discount": "",
                                //        "DiscountAmt": "",
                                //        "Discount2": "",
                                //        "DiscountAmt2": "",
                                //        "Discount3": "",
                                //        "DiscountAmt3": "",
                                //        "Discount4": "",
                                //        "DiscountAmt4": "",
                                //        "Discount5": "",
                                //        "DiscountAmt5": "",
                                //        "PriceBeforeDscnt": "",
                                //        "VatGroup": "SI",
                                //        "VatPercent": "12",
                                //        "VatSum": "",
                                //        "VatSumSys": "",
                                //        "VatSumFC": "",
                                //        "FinacialPeriodID": "",
                                //        "AcctCode": "",
                                //        "COGSAcct": "",
                                //        "LineTotal": "",
                                //        "LineTotalFC": "",
                                //        "LineTotalSC": "",
                                //        "LineVatSum": "",
                                //        "LinVatSys": "",
                                //        "LineVatFC": "",
                                //        "OpenSum": "",
                                //        "OpenSumFC": "",
                                //        "Factor1": "",
                                //        "Factor2": "",
                                //        "Factor3": "",
                                //        "Factor4": "",
                                //        "Length1": "",
                                //        "Len1Unit": "",
                                //        "Width1": "",
                                //        "Width1Unit": "",
                                //        "Height1": "",
                                //        "Height1Unit": "",
                                //        "Length2": "",
                                //        "Len2Unit": "",
                                //        "Width2": "",
                                //        "Width2Unit": "",
                                //        "Height2": "",
                                //        "Height2Unit": "",
                                //        "Volume": "",
                                //        "VolUnit": "",
                                //        "ProjectCode": "",
                                //        "OcrCode": "",
                                //        "OcrCode2": "",
                                //        "OcrCode3": "",
                                //        "OcrCode4": "",
                                //        "OcrCode5": "",
                                //        "GTotal": "",
                                //        "GTotalFC": "",
                                //        "GrossProfit": "",
                                //        "GrossProfitFC": "",
                                //        "VersionNo": "",
                                //    })
                            }


                            //$scope.$emit('onGridValidateItemCode', args)

                            break;

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

                    //$scope.$broadcast('onGridKeyDown', args)
                    //$scope.$emit('onGridKeyDown', { 'cell': e, 'args' : args})
                    $scope.$emit('onGridKeyDown', args)

                    $scope.$emit('onUpdateDocTotal', args)

                    grid.render();
                }
            }

            self.onDblClick = function (e, args) {
                let grid = args.grid
                $scope.$emit('onGridDblClick', args)
                //$scope.editRow({ row: args });
                grid.invalidate();
            }

            self.onBeforeEditCell = function (e, args) {
                let grid = args.grid;
                let field = grid.getColumns()[args.cell].field
                if (field === 'Linenum') {
                    return false;
                }

                //if (!isCellEditable(args.row, args.cell, args.item)) {
                //    return false;
                //}
            }

            self.onContextMenu = function (e, args) {
                let grid = args.grid 
                e.preventDefault();
                var cell = grid.getCellFromEvent(e);
                $("#gridContextMenu")
                    .data("data", { 'cell': cell, 'grid': grid, 'args': args })
                    .css("top", e.pageY - 30)
                    .css("left", e.pageX)
                    .css("display", "inline-block")
                    .show();

                $("body").on("click", function () {
                    $("#gridContextMenu").hide();
                });


                $scope.$emit('onGridContextMenu', { 'cell': cell, 'grid': grid, 'args': args })
            }

        }])
        .directive('cbDocFormGrid', ['$http', function ($http) {
            return {
                restrict: 'EA',
                controller: 'docFormGridCtrl',
                scope: {
                    gridColumns: '=gridColumns', //model
                    gridData: '=gridData',
                    gridOptions: '=gridOptions',
                    gridWithUpdatecol: '=gridWithUpdatecol',
                    gridRecordOption: '=gridRecordOption',
                    onAddRow: '&onAddRow',//function
                    onEditRow: '&onEditRow',
                    onDeleteRow: '&onDeleteRow',
                    onLoad: '&onLoad'
                },
                link: function ($scope, $element, $attrs, $ctrl) {
                    var grid;
                    var dv;

                    /*events*/
                    $scope.$watch('gridWithUpdatecol', function (newVal, oldVal) {
                        if (newVal) {
                            if (newVal === 'Y' && oldVal === 'N' || newVal === 'N' && oldVal === undefined ) {

                                $scope.gridWithUpdatecol = 'N'

                                grid = new Slick.Grid($element[0], $scope.gridData, $scope.gridColumns, $scope.gridOptions);

                                grid.init()

                                grid.onKeyDown.subscribe($ctrl.onKeyDownGrid);

                                grid.onDblClick.subscribe($ctrl.onDblClick);

                                grid.onBeforeEditCell.subscribe($ctrl.onBeforeEditCell);

                                grid.onContextMenu.subscribe($ctrl.onContextMenu);

                                $scope.$on('onDocumentSavePostAction', function (e, args) {
                                })
                            }
                        }
                    }, true);
                    /*end events */
                }
            }
        }])
}()); //formGridCtrl