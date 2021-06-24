(function () {
    let app = angular.module('FIN.RPT.TRNJOURNAL', [
        'cb.components.linktextbox',
        'cb.components.SelectList',
        'BP.SETUP.SERVICE',
        'FIN.MODELS',
        'FIN.DATASERVICE',
        'CB.TRNJOURNAL.GRID',
        'SLICKGRID.MODULE'
    ])

    app.controller('ReportFilterCtrl', [
        '$scope',
        '$rootScope',
        '$window',
        '$location',
        '$timeout',
        '$q',
        '$filter',
        '$uibModal',
        'journal.trntype.model',
        function ($scope, $rootScope, $window, $location, $timeout, $q, $filter, $uibModal, $trnType) {
            $scope.m = {}
            $scope.transactionTypeList = $trnType.JournalTransTypeList
            $scope.onBtnOkClick = (e) => {
                let trnType = $scope.m.TransType
                let fromDate = $filter('date')(new Date($scope.m.PostingFromDate), 'yyyyMMdd')
                let toDate = $filter('date')(new Date($scope.m.PostingToDate), 'yyyyMMdd')
                //$window.location.href = `${document.baseUrlNoArea}FIN/Finance/TransactionJournal/!#TrnJournalReportDetail/${trnType}/${fromDate}/${toDate}`
                //$window.location.href  = `${document.baseUrlNoArea}FIN/Finance/TransactionJournal!#/TrnJournalReportDetail/${trnType}`
                //$location.path(`TrnJournalReportDetail/${trnType}`)
                $location.path(`TrnJournalReportDetail/${trnType}/${fromDate}/${toDate}`)
            }
        }])
    app.controller('TrnJournalReportDetailCtrl', [
        '$scope',
        '$rootScope',
        '$window',
        '$timeout',
        '$location',
        '$q',
        '$routeParams',
        '$uibModal',
        'jen.formgrid.service',
        'fin.report.service',
        'slick.cell.formatter',
        'slick.total.formatter',
        function ($scope, $rootScope, $window, $timeout, $location, $q, $routeParams, $uibModal, $docFormGrid, $finReportSvc, $formatter, $totalFormatter) {


            self = this

            $scope.p = {}
            $scope.g = {}
            $scope.g.gridOptions = {}
            $scope.doc = {}

            $scope.p.transType = $routeParams.trnType
            $scope.p.fromDate = new Date(`${$routeParams.fromDate.substring(4, 6)}/${$routeParams.fromDate.substring(6, 8)}/${$routeParams.fromDate.substring(0, 4)}`)
            $scope.p.toDate = new Date(`${$routeParams.toDate.substring(4, 6)}/${$routeParams.toDate.substring(6, 8)}/${$routeParams.toDate.substring(0, 4)}`)

            $q.all([
                $docFormGrid.GetColumns('TJRPT', 'R').then(d => { return d.DataResult }),
                $finReportSvc.GetTransactionJournalReport($scope.p).then(r => { return r.DataResult })
            ]).then(r => {

                self.ColData = r[0]

                $scope.doc.CanEditGrid = 'N'
                $scope.g.gridData = r[1]


                let UpdateColumnEditor = function (d) {

                    if ($scope.recordMode === 'A' && self.Action === 'ADD') {
                        $scope.canAddEdit = true
                    }

                    if (d.originalname === 'TransID') {
                        d.headerCssClass = 'text-left'
                        d.cssClass = 'text-left'
                        //d.columnGroup =  "Tran No."
                    }

                    if (d.originalname === 'LineID') {
                        d.headerCssClass = 'text-left'
                        d.cssClass = 'text-left'
                    }


                    if (d.originalname === 'LineTotal' || d.originalname === 'LineTotalFC'
                        || d.originalname === 'Debit' || d.originalname === 'DebitFC' || d.originalname === 'DebitSC'
                        || d.originalname === 'Credit' || d.originalname === 'CreditFC' || d.originalname === 'CreditSC'
                    ) {
                        d.cssClass = 'text-right'
                        //d.editor = Slick.Editors.Float
                        d.headerCssClass = 'text-center'
                        d.formatter = $formatter.NumberFormatter
                        d.groupTotalsFormatter = $totalFormatter.GroupTotalFormatter
                    }

                    if (d.originalname === 'VatGroup') {
                        if ($scope.canAddEdit) {
                            d.editor = self.SelectListEditor
                            d.dataSource = res[0].DataResult
                        }
                        d.headerCssClass = 'text-center'
                        d.width = 150
                    }

                    if (d.originalname === 'TransType') {
                        d.headerCssClass = 'text-center'
                        d.cssClass = 'text-center'
                    }


                    if (d.originalname === 'RefDate') {
                        d.headerCssClass = 'text-center'
                        d.cssClass = 'text-center'
                        d.formatter = $formatter.DateFormatter
                    }


                    //if (d.originalname === 'TransID') {
                    //    d.headerCssClass = 'text-center'
                    //    d.formatter = $formatter.LinkFormatter
                    //}

                    if (d.originalname === 'GLAcctCode') {
                        d.headerCssClass = 'text-center'
                        d.formatter = $formatter.LinkFormatter
                    }

                    if (d.originalname === 'ShortName') {
                        if ($scope.canAddEdit) {
                            d.editor = self.LinkTextBoxEditor
                        }

                        d.formatter = $formatter.LinkFormatter
                        d.headerCssClass = 'text-center'
                    }

                    if (d.originalname === 'GLAcctName') {
                        d.headerCssClass = 'text-center'
                        //d.formatter = $formatter.LinkFormatter
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

                    $scope.g.withUpdateCol = 'Y'
                }

                UpdateColumns(self.ColData)

                $scope.onBtnBackClick = () => {
                    $location.path('TrnJournalReport')
                }

                //dataView.setItems(gData)
                //$scope.g.gridData = dataView;

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
        }])

}());

(function () {
    let app = angular.module('CB.TRNJOURNAL.GRID', ['FIN.DATASERVICE'])
    app.controller('TrnJournalReportGridCtrl', ['$scope', '$q', function ($scope, $q,) {

        var self = this;

        $scope.m = {};

        self.docLineID = $scope.$id

        $("#gridContextMenu").click(function (e) {

            e.preventDefault()

            var data = $(this).data("data");

            let grid = data.grid,
                args = data.args,
                gridData = grid.getData(),
                cell = data.cell,
                dupData = {}

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
                gridData.map(x => x.LineID = ++counter)
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
                    break;
                case 'DUPROW':

                    dupData = JSON.parse(JSON.stringify(gridData.slice(cell.row)[0]))

                    dupData.$id = self.getNextLineID()
                    dupData.DocID = 0;
                    dupData.DocLineID = 0;

                    gridData.splice(gridData[cell.row + 1], 0, dupData)
                    updateGrid()
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
                            //
                        }

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
                $scope.$emit('onGridKeyDown', { 'keyEvent': e, 'args': args })

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

            $("body").on("click", function () { $("#gridContextMenu").hide(); });


            $scope.$emit('onGridContextMenu', { 'cell': cell, 'grid': grid, 'args': args })
        }

        self.onRendered = function (e, args) { }

    }])
        .directive('cbTrnJournalGrid', ['$http', function ($http) {
            return {
                restrict: 'EA',
                controller: 'TrnJournalReportGridCtrl',
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
                            if (newVal === 'Y' && oldVal === 'N' || newVal === 'N' && oldVal === undefined) {

                                $scope.gridWithUpdatecol = 'N'



                                let dataView;
                                var groupItemMetadataProvider = new Slick.Data.GroupItemMetadataProvider();
                                dataView = new Slick.Data.DataView({
                                    groupItemMetadataProvider: groupItemMetadataProvider,
                                    inlineFilters: true
                                });


                                let gData = $scope.gridData.reduce((a, o) => {
                                    o['id'] = o.$id
                                    a.push(o)
                                    return a
                                }, [])

                                dataView.setItems(gData)

                                dataView.setGrouping([{
                                    getter: "TransID",
                                    formatter: function (g) {
                                        //return "TransID:  " + g.value + "  <span style='color:green'>(" + g.count + " items)</span>";
                                        //return "TransID:  " + g.value;
                                        return `<span class="text-left" onclick="onTransIdDrillDownClick(event, '${g.value}')"> Trans No. : <i class="fa fa-arrow-right"> ${g.value}</i></span>`;
                                    },
                                    aggregators: [
                                        new Slick.Data.Aggregators.Avg("TransID"),
                                        new Slick.Data.Aggregators.Sum("Debit"),
                                        new Slick.Data.Aggregators.Sum("Credit"),
                                    ],
                                    aggregateCollapsed: true,
                                    lazyTotalsCalculation: true
                                },
                                    //{
                                    //    getter: "Ref1",
                                    //    formatter: function (g) {
                                    //        //return "TransID:  " + g.value + "  <span style='color:green'>(" + g.count + " items)</span>";
                                    //        //return "TransID:  " + g.value;
                                    //        return `<span class="text-left" onclick="onTransIdDrillDownClick(event, '${g.value}')"> Trans No. : <i class="fa fa-arrow-right"> ${g.value}</i></span>`;
                                    //    },
                                    //    aggregators: [
                                    //        new Slick.Data.Aggregators.Avg("TransID"),
                                    //        new Slick.Data.Aggregators.Sum("Debit"),
                                    //        new Slick.Data.Aggregators.Sum("Credit"),
                                    //    ],
                                    //    aggregateCollapsed: false,
                                    //    lazyTotalsCalculation: true
                                    //}



                                ]);



                                //grid = new Slick.Grid($element[0], $scope.gridData, $scope.gridColumns, $scope.gridOptions);

                                grid = new Slick.Grid($element[0], dataView, $scope.gridColumns, $scope.gridOptions);

                                // register the group item metadata provider to add expand/collapse group handlers
                                grid.registerPlugin(groupItemMetadataProvider);
                                grid.setSelectionModel(new Slick.CellSelectionModel());

                                grid.init()

                                grid.onKeyDown.subscribe($ctrl.onKeyDownGrid);

                                grid.onDblClick.subscribe($ctrl.onDblClick);

                                grid.onBeforeEditCell.subscribe($ctrl.onBeforeEditCell);

                                grid.onContextMenu.subscribe($ctrl.onContextMenu);

                                grid.onRendered.subscribe($ctrl.onRendered);

                                $scope.$emit('onLoadGrid', grid)

                                /*received events*/
                                $scope.$on('onDocumentSavePostAction', function (e, args) { })
                            }
                        }
                    }, true);
                    /*end events */
                }
            }
        }])
}()); //formGridCtrl

(function () {
    let app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['ngRoute', 'FIN.RPT.TRNJOURNAL'])
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/TrnJournalReport', {
                templateUrl: 'trnJournalReportFilter.html',
                controller: 'ReportFilterCtrl'
            })

            .when('/TrnJournalReportDetail/:trnType/:fromDate/:toDate', {
                templateUrl: 'trnJournalReportDetail.html',
                controller: 'TrnJournalReportDetailCtrl'
            })

            .otherwise({
                templateUrl: 'trnJournalReportFilter.html',
                controller: 'ReportFilterCtrl'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');
    }])

}())


