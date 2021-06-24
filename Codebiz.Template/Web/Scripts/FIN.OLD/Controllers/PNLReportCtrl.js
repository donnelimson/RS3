(function () {

    let app = angular.module('FIN.RPT.PNL', [
        'cb.components.linktextbox',
        'cb.components.SelectList',
        'BP.SETUP.SERVICE',
        'FIN.MODELS',
        'FIN.DATASERVICE',
        'CB.PNL.GRID',
        'slickgrid.module'
    ])

    app.controller('ReportFilterCtrl', ['$scope',
        '$rootScope',
        '$window',
        '$location',
        '$timeout',
        '$q',
        '$filter',
        '$uibModal',
        function ($scope, $rootScope, $window, $location, $timeout, $q, $filter, $uibModal) {

            $scope.m = {}
            $scope.m.RptType = "A";
            $scope.m.DateBy = "P";


            $scope.onBtnOkClick = () => {

                let dateBy = $scope.m.DateBy,
                    fromDate = $filter('date')(new Date($scope.m.DateFrom), 'yyyyMMdd'),
                    toDate = $filter('date')(new Date($scope.m.DateTo), 'yyyyMMdd'),
                    reportType = $scope.m.RptType;
                $location.path(`PNLReportDetail/${dateBy}/${reportType}/${fromDate}/${toDate}`)
            }


        }])
        .controller('PNLReportDetailCtrl', [
            '$scope',
            '$rootScope',
            '$window',
            '$location',
            '$timeout',
            '$q',
            '$routeParams',
            '$uibModal',
            '$filter',
            'jen.formgrid.service',
            'fin.report.service',
            'slick.cell.formatter',
            'slick.total.formatter',
            function ($scope, $rootScope, $window, $location, $timeout, $q, $routeParams, $uibModal, $filter, $docFormGrid, $finReportSvc,  $formatter, $totalFormatter) {

                self = this

                $scope.p = {}
                $scope.g = {}
                $scope.g.gridOptions = {}
                $scope.doc = {}

                $scope.p.dateType = $routeParams.dateType
                $scope.p.reportType = $routeParams.rptType

                $scope.p.fromDate = new Date(`${$routeParams.fromDate.substring(4, 6)}/${$routeParams.fromDate.substring(6, 8)}/${$routeParams.fromDate.substring(0, 4)}`)
                $scope.p.toDate = new Date(`${$routeParams.toDate.substring(4, 6)}/${$routeParams.toDate.substring(6, 8)}/${$routeParams.toDate.substring(0, 4)}`)

                $q.all([
                    $docFormGrid.GetColumns(`PNLR${$scope.p.reportType}`, 'R').then(d => { return d.DataResult }),
                    $finReportSvc.GetPNLReport($scope.p).then(r => { return r.DataResult })
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


                        if (d.originalname === 'CurrentPeriod' || d.originalname === 'CurrentYear'
                            || d.originalname === 'Q1' || d.originalname === 'Q2' || d.originalname === 'Q3' || d.originalname === 'Q4'
                            || d.originalname === 'Jan' || d.originalname === 'Feb' || d.originalname === 'Mar'
                            || d.originalname === 'Apr' || d.originalname === 'May' || d.originalname === 'Jun'
                            || d.originalname === 'Jul' || d.originalname === 'Aug' || d.originalname === 'Sep'
                            || d.originalname === 'Oct' || d.originalname === 'Nov' || d.originalname === 'Dec'
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

                        if (d.originalname === 'AcctCode') {
                            d.headerCssClass = 'text-left'
                            d.formatter = $formatter.GroupFormatter
                        }


                        if (d.originalname === 'AcctName') {
                            d.headerCssClass = 'text-left'
                            d.formatter = $formatter.GroupFormatter
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

                    $scope.onSelectChange = function () {
                        $scope.g.gridData = r[1].filter(x => x.Level <= parseInt($scope.m.Level))
                        $scope.g.withUpdateCol = 'Y'
                        UpdateColumns(self.ColData)
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
    let app = angular.module('CB.PNL.GRID', ['FIN.DATASERVICE'])
    app.controller('PNLReportGridCtrl', ['$scope', '$q', function ($scope, $q,) {

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
                controller: 'PNLReportGridCtrl',
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
                                //var groupItemMetadataProvider = new Slick.Data.GroupItemMetadataProvider();
                                dataView = new Slick.Data.DataView({
                                    //groupItemMetadataProvider: groupItemMetadataProvider,
                                    inlineFilters: true
                                });


                                let gData = $scope.gridData.reduce((a, o) => {
                                    o['id'] = o.$id
                                    a.push(o)
                                    return a
                                }, [])

                                //dataView.setItems(gData)

                                for (let x = 0; x < gData.length; x++) {
                                    if (gData[x].FatherCode === undefined || gData[x].FatherCode === '') {
                                        gData[x].parent = null
                                        gData[x].indent = 0
                                    }
                                    else {
                                        let item = gData[x]
                                        let parent = gData.filter(q => q.AcctCode === item.FatherCode)[0]
                                        if (parent) {
                                            gData[x].parent = gData.indexOf(parent);
                                            gData[x].indent = item.Level
                                        }
                                    }

                                }
                                

                                dataView.beginUpdate();
                                dataView.setItems(gData);
                                dataView.setFilter(function (i) {
                                    return true;
                                });
                                dataView.endUpdate();

                                //dataView.setGrouping([{
                                //    getter: "FatherCode",
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
                                //    aggregateCollapsed: true,
                                //    lazyTotalsCalculation: true
                                //}
                                //]);

                                grid = new Slick.Grid($element[0], dataView, $scope.gridColumns, $scope.gridOptions);
                                //grid = new Slick.Grid($element[0], $scope.gridData, $scope.gridColumns, $scope.gridOptions);

                                // register the group item metadata provider to add expand/collapse group handlers
                                //grid.registerPlugin(groupItemMetadataProvider);

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
}());

(function () {
    let app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['ngRoute', 'FIN.RPT.PNL'])
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/PNLReport', {
                templateUrl: 'PNLReportFilter.html',
                controller: 'ReportFilterCtrl'
            })

            .when('/PNLReportDetail/:dateType/:rptType/:fromDate/:toDate', {
                templateUrl: 'PNLReportDetail.html',
                controller: 'PNLReportDetailCtrl'
            })

            .otherwise({
                templateUrl: 'PNLReportFilter.html',
                controller: 'ReportFilterCtrl'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');
    }])

}());
