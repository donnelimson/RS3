(function () {
    let app = angular.module('cb.doc.InvntryTrackingGrid', [])

    app
        .controller('InvntryTrackingGridCtrl', ['$scope', '$q',  function ($scope, $q) {

            var self = this;

            $scope.m = {};

            self.docLineID = $scope.$id

            self.deleteTask = function (data, row) {
                data.splice(row.row, 1);
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
                                //todo
                            }

                            //$scope.$emit('onGridValidateItemCode', args)

                            break;

                        case 'WhsCode':
                            break;

                        case 'Price':
                            //self.ComputeLineTotal(args)
                            break;
                        case 'Quantity':
                            //self.ComputeLineTotal(args)
                            break;

                        case 'VatGroup':
                            //self.ComputeLineTotal(args)
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
                controller: 'InvntryTrackingGridCtrl',
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