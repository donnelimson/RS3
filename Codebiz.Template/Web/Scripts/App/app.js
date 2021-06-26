/***
Metronic AngularJS App Main Script
***/
/* ENUMS */
var FILE_TYPE = {
    Image_JPEG: { Value: 1, Desc: "image/jpg" },
    Image_PNG: { Value: 2, Desc: "image/png" },
    Application_PDF: { Value: 3, Desc: "application/pdf" },
    Application_MSWORD: { Value: 4, Desc: "application/msword" },
    Docx: { Value: 5, Desc: "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
};
var ACCOUNT_TYPE = {
    Permanent: { Value: 1, Desc: "P", Val: "Permanent"},
    Special_Lighting: { Value: 2, Desc: "SPL", Val: "Special Lighting"},
    SoleUse: { Value: 3, Desc: "SU", Val: "Sole Use"}
}
var TRANSACTION_TYPE = {
    Member_Application: { Value: 1, Desc: "Member Account Application" },
    Change_of_Name: { Value: 2, Desc: "Change of Name" },
    Discount_Application: { Value: 3, Desc: "Discount Application" },
    Burial_Assistance: { Value: 4, Desc: "Burial Assistance" },
    //Reconnection: { Value: 5, Desc: "Reconnection" },
    //Close_and_Transfer: { Value: 6, Desc: "Close and Transfer" },
    //Disconnection: { Value: 7, Desc: "Disconnection" },
    Net_Metering: { Value: 8, Desc: "NetMetering" },
    Item_Master_Data: { Value: 9, Desc: "Item Master Data" },

    Job_Order_Complaint: { Value: 10, Desc: "Job Order - Complaint" },
    For_Payment: { Value: 11, Desc: "For Payment" },
    Job_Order_Request: { Value: 12, Desc: "Job Order - Request" },

    Discount_Application_Renewal: { Value: 13, Desc: "Discount Application Renewal" },

    Inventory_Transfer: { Value: 17, Desc: "Inventory Transfer" },
    Job_Order_Receive: { Value: 19, Desc: "Job Order - Receive" },
    BP_Customer: { Value: 18, Desc: "BP Customer" },
    BP_Vendor: { Value: 54, Desc: "BP Vendor" },
    Inventory_Receiving: { Value: 37, Desc: "Inventory Receiving" },
    Job_Order_Request_Indirect: { Value: 38, Desc: "Job Order Request - Indirect" },
    Job_Order_Request_Direct: { Value: 39, Desc: "Job Order Request - Direct" },
    Job_Order_Complaint_Indirect: { Value: 40, Desc: "Job Order Complaint - Indirect" },
    Job_Order_Complaint_Direct: { Value: 41, Desc: "Job Order Complaint - Direct" },
    Outgoing_Distribution_Transformer: { Value: 42, Desc: "Outgoing Distribution Transformer" },
    Bill_Of_Materials: { Value: 50, Desc: "Bill of Materials" },
    Vehicle_Inspections: { Value: 15, Desc: "Vehicle Inspections" },
    Vehicle_Request: { Value: 14, Desc: "Vehicle request" },
    Meter_Withdrawal: { Value: 51, Desc: "Meter Withdrawal" },
    Other_Request: { Value: 43, Desc: "Other Request" },
    Material_Request: { Value: 52, Desc: "Material Request" },
    Contestable_Application: { Value: 53, Desc: "Contestable Application" },
    Stock_Requisition_Voucher: { Value: 55, Desc: "Stock Requisition Voucher" },
    Transformer_Rental: { Value: 57, Desc: "Transformer Rental" },
    Purchase_Requisition_Voucher: { Value: 58, Desc: "Purchase Requisition Voucher" },
    Approval: { Value: 59, Desc: "Approval" },
    Change_of_Meter: { Value: 60, Desc: "Change of Meter" },
    Special_Lighting: { Value: 61, Desc: "Special Lighting Application" },
    Coop_Vehicle: { Value: 63, Desc: "Coop Vehicle" },
    Billing_Adjustment: { Value: 45, Desc: "Billing Adjustment" },
};

var TRANSACTION_SUBTYPE = {
    DiscountApplication_ResidentialSeniorCitizen: { Value: 1, Desc: "Residential Senior Citizen" },
    DiscountApplication_DSWDAccreditedSenioCitizen: { Value: 2, Desc: "DSWD - Accredited Senior Citizen Centers/Residential Care Institutions" },
    MemberApplication_Small: { Value: 3, Desc: "Small" },
    MemberApplication_Big: { Value: 4, Desc: "Big" },
    ChangeOfName_Sale: { Value: 5, Desc: "Sale" },
    ChangeOfNamee_Waived: { Value: 6, Desc: "Waived" },
    ChangeOfName_Death: { Value: 7, Desc: "Death" },
    ChangeOfName_ChangeStatus: { Value: 8, Desc: "Change Status" },
};

var VALIDATIONS = {
    IsWithPendingJO: { Value: 2, Msg: 'Account still has pending job order.' },
    HasUnpaidInvoices: { Value: 3, Msg: 'Account has unpaid invoice/s.' },
    IsInvalid: { Value: 4, Msg: 'Account is invalid due to following reason/s.' },
    NonExisting: { Value: 5, Msg: "Account doesn't exist." },
    AcctTransNonExisting: { Value: 6, Msg: "Account transaction doesn't exist." },
    ReferenceCodeNonExisting: { Value: 7, Msg: "Reference code doesn't exist." },

};

var JOTASKTOBEPERFORM = {
    Testing: { Value: 1, Desc: "Testing" },
    Installation: { Value: 2, Desc: "Installation" },
    Inspection: { Value: 3, Desc: "Inspection" },
    Fixing: { Value: 4, Desc: "Fixing" },
    Pullout: { Value: 5, Desc: "Pull-out" },
    Reconnection: { Value: 6, Desc: "Reconnection" },
    TemporaryDisconnection: { Value: 7, Desc: "Temporary Disconnection" },
    PermanentDisconnection: { Value: 8, Desc: "Permanent Disconnection" },
}

var JONATURETYPE = {
    KHWMeter: { Value: 1, Desc: "KWH Meter" },
    Pole: { Value: 2, Desc: "Pole" },
    Transformer: { Value: 3, Desc: "Transformer" },
}

var DIVISIONCATEGORY = {
    Metering: { Value: 1, Desc: "Metering" },
    Transformer: { Value: 2, Desc: "Transformer" },
    Planning: { Value: 3, Desc: "Planning" },
    Construction: { Value: 4, Desc: "Construction" },
}

var DECISION = {
    RejectHouseWiringInspection: { Value: 4, Desc: "Reject House Wiring Inspection" },
    DisapproveHouseWiringInspection: { Value: 3, Desc: "Disapprove House Wiring Inspection" }
}

var MEMBERSHIP_TYPE = {
    Natural: { Value: 1, Desc: "Natural" },
    Juridical: { Value: 2, Desc: "Juridical" }
}

var MEMBERSHIP_SUBTYPE = {
    Single: { Value: 1, Desc: "Single" },
    Joint: { Value: 2, Desc: "Joint" },
    Corporation: { Value: 3, Desc: "Corporation" },
    Association: { Value: 4, Desc: "Association" },
    Agency: { Value: 5, Desc: "Agency" },
    SoleProprietor: { Value: 6, Desc: "Sole Proprietor" },
    Cooperative: { Value: 7, Desc: "Cooperative" },
    Partnership: { Value: 8, Desc: "Partnership" }
}

var POSITION = {
    HouseWiringInspector: { Value: 1, Desc: "HW INSPECTOR" },
    Lineman: { Value: 2, Desc: "LINEMAN" },
    ComSectionHead: { Value: 3, Desc: "COM SECTION HEAD" },
    Teller: { Value: 4, Desc: "TELLER" },
    GeneralManager: { Value: 5, Desc: "GENERAL MANAGER" },
    MeterReader: { Value: 6, Desc: "METER READER" }
}


var ITEM_TYPE = {
    Item: { Value: 1, Desc: "I" },
    Labor: { Value: 1, Desc: "L" },
    Travel: { Value: 1, Desc: "T" },
}

var UOM_TYPE = {
    Sales: { Value: 1, Desc: "SAL" },
    Purchase: { Value: 2, Desc: "PUR" },
    Inventory: { Value: 3, Desc: "INVNTRY" },
}

//RS3
var PRIORITIES = [
    { 'Description': '1' },
    { 'Description': '2' },
    { 'Description': '3' },
];
var TICKETSTATUSES = [
    { 'Description': 'Open', 'Code':'O' },
    { 'Description': 'Resolved', 'Code':'R' },
];
/* Metronic App */
var MetronicApp = angular.module("MetronicApp", [
    "ngSanitize",
    "ui.bootstrap",
    "ui.validate",
    'ngTable',
    'blueimp.fileupload',
    'cfp.hotkeys',
    'daterangepicker',
    'webcam',
    'ngMap',
    'ui.sortable',
    'ngIdle',
    'angular.filter',
    'blockUI',
    'bcherny/formatAsCurrency'
]);

var EventsApp = angular.module("EventsApp", ['ngIdle']);
MetronicApp.config(['KeepaliveProvider', 'IdleProvider', function (KeepaliveProvider, IdleProvider) {
    IdleProvider.idle(5);
    IdleProvider.timeout(19 * 60);
    KeepaliveProvider.interval(20);
}]);

MetronicApp.controller('EventsController', function ($scope, Idle, $window, $uibModal, $controller) {
    $scope.events = [];
    $scope.idle = 5;
    $scope.timeout = 19 * 60;

    $scope.$on('IdleTimeout', function () {
        $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'warning-dialog.html',
            controller: "ModalEventsController",
            size: 'md',
            keyboard: false,
            backdrop: "static",
            windowClass: 'modal_style',
            resolve: {
                Idle: function () {
                    return Idle;
                },
            }
        }, function () {
        });
    })
})

MetronicApp.controller('ModalEventsController', function ($scope, $uibModalInstance, $timeout, $interval, Idle) {

    countdown(60);
    function countdown(current_time) {
        if (current_time === 0) {
            return;
        }
        else {
            current_time--;
        }
        $scope.time = current_time;
        $timeout(function () { countdown(current_time) }, 1000);
    }

    $scope.timer = 1;
    $scope.max = 60;
    var progressBar = $interval(function () {
        $scope.timer++;
        if ($scope.timer <= 0) {
            $interval.cancel(progressBar);
        }
    }, 1000);

    $scope.stayConnected = function () {
        Idle.watch();
        $uibModalInstance.dismiss('cancel');
        $timeout.cancel(timer);
    };

    $scope.logout = function () {
        document.getElementById('logoutForm').submit();
    };
    timer = $timeout(callAtTimeout, 60000);
    function callAtTimeout() {
        document.getElementById('logoutForm').submit();
    }

})
    .config(function (IdleProvider, KeepaliveProvider) {
        KeepaliveProvider.interval(20);
        IdleProvider.windowInterrupt('focus');
    })
    .run(function (Idle, $log) {
        Idle.watch();
        $log.debug('app started');
    });

//AngularJS v1.3.x workaround for old style controller declarition in HTML
MetronicApp.config(['$controllerProvider', function ($controllerProvider) {
    // this option might be handy for migrating old apps, but please don't use it
    // in new ones!
    $controllerProvider.allowGlobals();
}]);

MetronicApp.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);

//File Upload Config
MetronicApp.config([
    '$httpProvider', 'fileUploadProvider',
    function ($httpProvider, fileUploadProvider) {
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
        fileUploadProvider.defaults.redirect = window.location.href.replace(
            /\/[^\/]*$/,
            '/cors/result.html?%s'
        );

        angular.extend(fileUploadProvider.defaults, {
            // Enable image resizing, except for Android and Opera,
            // which actually support image resizing, but fail to
            // send Blob objects via XHR requests:
            //disableImageResize: /Android(?!.*Chrome)|Opera/
            //    .test(window.navigator.userAgent),
            autoUpload: true,
            maxFileSize: 20000000,//20MB
            acceptFileTypes: /(\.|\/)(jpe?g|png|docx|pdf)$/i
        });
    }
]);


/********************************************
 END: BREAKING CHANGE in AngularJS v1.3.x:
*********************************************/

/* Setup global settings */
MetronicApp.factory('settings', ['$rootScope', function ($rootScope) {
    // supported languages
    var settings = {
        layout: {
            pageSidebarClosed: false, // sidebar menu state
            pageContentWhite: true, // set page content layout
            pageBodySolid: false, // solid body color state
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        assetsPath: '../assets',
        globalPath: '../assets/global',
        layoutPath: '../assets/layouts/layout',
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* Setup App Main Controller */
MetronicApp.controller('AppController', ['$scope', '$rootScope', function ($scope, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        //App.initComponents(); // init core components
        //Layout.init(); //  Init entire layout(header, footer, sidebar, etc) on page load if the partials included in server side instead of loading with ng-include directive 
    });

    $scope.message = "Hello world!";
}]);

MetronicApp.factory('FileCounter', function () {
    return {
        count: 0,
        currentFileCount: 0,
        validTypes: ["application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "image/jpeg", "image/png", "application/pdf", "application/msword", "image/gif"],
        isInitialize: false
    };
});

/***
GLobal Directives
***/

//// Handle Dropdown Hover Plugin Integration
MetronicApp.directive('dropdownMenuHover', function () {
    return {
        link: function (scope, elem) {
            elem.dropdownHover();
        }
    };
});

MetronicApp.directive('restrictTo', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var re = RegExp(attrs.restrictTo);
            var exclude = /Backspace|Enter|Tab|Delete|Del|ArrowUp|Up|ArrowDown|Down|ArrowLeft|Left|ArrowRight|Right/;

            element[0].addEventListener('keydown', function (event) {
                if (!exclude.test(event.key) && !re.test(event.key)) {
                    event.preventDefault();
                }
            });
        }
    }
});

MetronicApp.directive('customOnChange', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var onChangeHandler = scope.$eval(attrs.customOnChange);
            element.on('change', onChangeHandler);
            element.on('$destroy', function () {
                element.off();
            });

        }
    };
});

MetronicApp.directive('tinNumber', function () {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs, ctrl) {
            elm.on('keydown', function (event) {
                var $input = $(this);
                var value = $input.val();
                var inputLength = value.length;
                value = value.replace(/[^\d-]+/g, '');
                $input.val(value);

                if (inputLength === 3 || inputLength === 7 || inputLength === 11) {
                    $input.val(value + "-");
                }

                if (event.which === 8 && (inputLength === 4 || inputLength === 8 || inputLength === 12)) {
                    $input.val(value.substring(0, value.length - 1));
                }

                if (event.which === 64 || event.which === 16) {
                    // to allow numbers  
                    return false;
                } else if (event.which >= 48 && event.which <= 57) {
                    // to allow numbers  
                    return true;
                } else if (event.which >= 96 && event.which <= 105) {
                    // to allow numpad number  
                    return true;
                } else if ([8, 9, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                    // to allow backspace, enter, escape, arrows  
                    return true;
                } else {
                    event.preventDefault();
                    // to stop others  
                    //alert("Sorry Only Numbers Allowed");  
                    return false;
                }
            });
        }
    }
});

MetronicApp.directive('allowOnlyNumbers', function () {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs, ctrl) {
            elm.on('keydown', function (event) {
                var $input = $(this);
                var value = $input.val();
                var inputLength = value.length;
                value = value.replace(/[^\d-]+/g, '');
                $input.val(value);

                if (event.which === 8 && (inputLength === 4 || inputLength === 8 || inputLength === 12)) {
                    $input.val(value.substring(0, value.length - 1));
                }

                if (event.which === 64 || event.which === 16) {
                    // to allow numbers  
                    return false;
                } else if (event.which >= 48 && event.which <= 57) {
                    // to allow numbers  
                    return true;
                } else if (event.which >= 96 && event.which <= 105) {
                    // to allow numpad number  
                    return true;
                } else if ([8, 9, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                    // to allow backspace, enter, escape, arrows  
                    return true;
                } else {
                    event.preventDefault();
                    // to stop others  
                    //alert("Sorry Only Numbers Allowed");  
                    return false;
                }
            });
        }
    }
});

MetronicApp.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});

MetronicApp.directive("loadingContainer", function () {
    return {
        restrict: "A",
        scope: false,
        link: function (scope, element, attrs) {
            //var loadingLayer = angular.element("<div class='spinner'> <div></div> <div></div> <div></div> <div></div> <div></div> <div></div> <div></div> <div></div> <div></div> <div></div> <div></div> <div></div> </div>");
            var loadingLayer = angular.element("<div class='loading-overlay'> <div class='overlay'></div> <div class='spinner'></div> </div>");
            //element.append(loadingLayer);
            element.prepend(loadingLayer);
            element.addClass("loading-container");
            scope.$watch(attrs.loadingContainer,
                function (value) {
                    loadingLayer.toggleClass("ng-hide", !value);
                });
        }
    };
});

MetronicApp.directive("select2", function ($timeout, $parse) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs) {
            $timeout(function () {
                element.select2({
                    placeholder: "---Please select---",
                    allowClear: !element[0].multiple
                });
                element.select2Initialized = true;
            });

            var refreshSelect = function (newVal, oldVal) {
                if (!element.select2Initialized) {
                    if (oldVal != null &&
                        ((!element[0].multiple && oldVal != newVal) ||
                            (element[0].multiple && JSON.stringify(oldVal) != JSON.stringify(newVal)))) {
                        $timeout(function () {
                            element.trigger('change');
                        });
                    }
                    else {
                        return;
                    }
                }
                else {
                    element.select2Initialized = false;
                    $timeout(function () {
                        element.trigger('change');
                    });
                }
            };

            var recreateSelect = function () {
                if (!element.select2Initialized) return;
                element.select2Initialized = true;
                $timeout(function () {
                    element.select2('destroy');
                    element.select2({
                        placeholder: "---Please select---",
                        allowClear: !element[0].multiple
                    });
                });
            };

            scope.$watch(attrs.ngModel, refreshSelect);

            if (attrs.ngOptions) {
                var list = attrs.ngOptions.match(/ in ([^ ]*)/)[1];
                // watch for option list change
                scope.$watch(list, recreateSelect);
            }

            if (attrs.ngDisabled) {
                scope.$watch(attrs.ngDisabled, refreshSelect);
            }
        }
    };
});

MetronicApp.directive('yearpicker', ['$timeout',
    function ($timeout) {
        var link;
        link = function (scope, element, attr, ngModel) {
            element = $(element);
            console.log(scope[attr.ngModel])
            element.datepicker({
                format: 'yyyy',
                minViewMode: "years",
                startView: "years",
                defaultDate: scope[attr.ngModel],
                orientation: 'bottom',
                autoclose: true,
                onSelect: function (dateText) {
                    scope.$apply(function () {
                        ngModel.$setViewValue(dateText);
                    });
                }
            });
        };

        return {
            restrict: 'A',
            link: link,
            require: '?ngModel'
        };
    }
]);

MetronicApp.directive('timepicker', ['$timeout',

    function ($timeout) {
        var link;
        link = function (scope, element, attr, ngModel) {
            element = $(element);
            var dp_el = element.datetimepicker({
                pickDate: false,
                pickerPosition: 'top-left',
                minuteStep: 2,
                format: 'HH:mm:ss p',
                autoclose: true,
                showSeconds: true,
                showMeridian: true,
                startView: 1,
                maxView: 1,
                widgetPositioning: { horizontal: 'auto', vertical: 'bottom' }
            });

            $(".datetimepicker").find('thead th').remove();
            $(".datetimepicker").find('thead').append($('<th class="switch">').text('Pick Time'));
            $('.switch').css('width', '190px');

            dp_el.on('changeDate', function (event) {
                //scope.$apply(function () {
                //    ngModel.$setViewValue(event.date._d);
                //});

                if (event.target.tagName !== 'INPUT') {
                    ngModel.$setViewValue(event.date._d.format('H:mm:ss p'));
                    ngModel.$render();
                }
            });
            dp_el.on('date-reset', function (event) {
                alert("ok")
            });
        };

        return {
            restrict: 'A',
            link: link,
            require: '?ngModel'
        };
    }
])

MetronicApp.directive('datetimepicker', ['$timeout',

    function ($timeout) {
        var link;
        link = function (scope, element, attr, ngModel) {
            element = $(element);
            var dp_el = element.datetimepicker({
                format: 'mm/dd/yyyy HH:ii P',
                endDate: new Date(),
                autoclose: true,
                defaultDate: scope[attr.ngModel],
                showMeridian: true,
                widgetPositioning: { horizontal: 'auto', vertical: 'bottom' }
            });
            dp_el.on('changeDate', function (event) {
                //scope.$apply(function () {
                //    ngModel.$setViewValue(event.date._d);
                //});

                if (event.target.tagName !== 'INPUT') {
                    ngModel.$setViewValue(event.date._d.format('mm/dd/yyyy HH:ii P'));
                    ngModel.$render();
                }
            });
            dp_el.on('date-reset', function (event) {
                alert("ok")
            });
        };

        return {
            restrict: 'A',
            link: link,
            require: '?ngModel'
        };
    }
])

MetronicApp.directive('reversedatetimepicker', ['$timeout',

    function ($timeout) {
        var link;
        link = function (scope, element, attr, ngModel) {
            element = $(element);
            var dp_el = element.datetimepicker({
                format: 'mm/dd/yyyy HH:ii p',
                startDate: new Date(),
                autoclose: true,
                defaultDate: scope[attr.ngModel],
                showMeridian: true,
                widgetPositioning: { horizontal: 'auto', vertical: 'bottom' }
            });
            dp_el.on('changeDate', function (event) {
                //scope.$apply(function () {
                //    ngModel.$setViewValue(event.date._d);
                //});

                if (event.target.tagName !== 'INPUT') {
                    ngModel.$setViewValue(event.date._d.format('mm/dd/yyyy HH:ii p'));
                    ngModel.$render();
                }
            });
            dp_el.on('date-reset', function (event) {
                alert("ok")
            });
        };

        return {
            restrict: 'A',
            link: link,
            require: '?ngModel'
        };
    }
])

MetronicApp.directive('datePicker', function () {
    var link;
    link = function (scope, element, attr, ngModel) {
        element = $(element);
        element.datetimepicker({
            format: 'YYYY-MM-DD HH:mm',
            defaultDate: scope[attr.ngModel]
        });
        element.on('dp.change', function (event) {
            scope.$apply(function () {
                ngModel.$setViewValue(event.date._d);
            });
        });
    };

    return {
        restrict: 'A',
        link: link,
        require: 'ngModel'
    };
});

MetronicApp.directive('datePickerBottom', function () {
    var link;
    link = function (scope, element, attr, ngModel) {
        element = $(element);
        element.datepicker({
            format: 'mm/dd/yyyy',
            defaultDate: scope[attr.ngModel],
            orientation: 'bottom',
            autoclose: true,

        });
        element.on('dp.change', function (event) {
            scope.$apply(function () {
                ngModel.$setViewValue(event.date._d);
            });
        });
    };

    return {
        restrict: 'A',
        link: link,
        require: 'ngModel'
    };
});

MetronicApp.directive('datePickerDateOnlyTop', function () {
    var link;
    link = function (scope, element, attr, ngModel) {
        element = $(element);
        element.datepicker({
            format: 'mm/dd/yyyy',
            defaultDate: scope[attr.ngModel],
            orientation: 'top',
            autoclose: true,

        })
        element.on('change', function (event) {
            if (scope.$root.$$phase != '$apply' && scope.$root.$$phase != '$digest') {
                scope.$apply(function () {
                    scope.triggerChange();
                });
            }
            else { scope.triggerChange(); }


        });
    };

    return {
        restrict: 'A',
        link: link,
        require: 'ngModel',
        scope: { triggerChange: '&' }
    };
});


MetronicApp.directive('datePickerDateOnly', function () {
    var link;
    link = function (scope, element, attr, ngModel) {
        element = $(element);
        element.datepicker({
            format: 'mm/dd/yyyy',
            defaultDate: scope[attr.ngModel],
            orientation: 'bottom',
            autoclose: true,

        })
        element.on('change', function (event) {
            if (scope.$root.$$phase != '$apply' && scope.$root.$$phase != '$digest') {
                scope.$apply(function () {
                    scope.triggerChange();
                });
            }
            else { scope.triggerChange(); }


        });
    };

    return {
        restrict: 'A',
        link: link,
        require: 'ngModel',
        scope: { triggerChange: '&' }
    };
});

MetronicApp.directive('dateRange', function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            $(elem).daterangepicker({
                opens: 'left',
                autoUpdateInput: false,
                locale: {
                    cancelLabel: 'Clear'
                }
            }, function (start, end, label) {
            });

            $(elem).on('apply.daterangepicker', function (ev, picker) {
                $(elem).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
                $(elem).trigger('change');
            });

            $(elem).on('cancel.daterangepicker', function (ev, picker) {
                $(elem).val('');
                $(elem).trigger('change');
            });
        }
    };
});

MetronicApp.directive('dateRangeright', function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            $(elem).daterangepicker({
                opens: 'right',
                autoUpdateInput: false,
                locale: {
                    cancelLabel: 'Clear'
                }
            }, function (start, end, label) {
            });

            $(elem).on('apply.daterangepicker', function (ev, picker) {
                $(elem).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
                $(elem).trigger('change');
            });

            $(elem).on('cancel.daterangepicker', function (ev, picker) {
                $(elem).val('');
                $(elem).trigger('change');
            });
        }
    };
});
MetronicApp.directive('dateRangerightWithMinDate', function () {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            //var currentTime = new Date();
            //var startDateFrom = new Date(currentTime.getFullYear(), currentTime.getMonth(), 1);
            $(elem).daterangepicker({
                opens: 'right',
                autoUpdateInput: false,
                minDate: moment(),
                locale: {
                    cancelLabel: 'Clear'
                }
            }, function (start, end, label) {
            });

            $(elem).on('apply.daterangepicker', function (ev, picker) {
                $(elem).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
                $(elem).trigger('change');
            });

            $(elem).on('cancel.daterangepicker', function (ev, picker) {
                $(elem).val('');
                $(elem).trigger('change');
            });
        }
    };
});

MetronicApp.directive('scrollToBottom', ['$timeout',
    function ($timeout) {
        return {
            scope: {
                scrollToBottom: "="
            },
            restrict: 'A',
            link: function (scope, element, attr) {
                scope.$watchCollection('scrollToBottom', function (newVal) {
                    if (newVal) {
                        $timeout(function () {
                            element[0].scrollTop = element[0].scrollHeight;
                        }, 0);
                    }

                });
            }
        };
    }
]);

MetronicApp.directive('capitalize', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            var capitalize = function (inputValue) {
                if (inputValue == undefined) inputValue = '';
                var capitalized = inputValue.toUpperCase();
                if (capitalized !== inputValue) {
                    // see where the cursor is before the update so that we can set it back
                    var selection = element[0].selectionStart;
                    modelCtrl.$setViewValue(capitalized);
                    modelCtrl.$render();
                    // set back the cursor after rendering
                    element[0].selectionStart = selection;
                    element[0].selectionEnd = selection;
                }
                return capitalized;
            }
            modelCtrl.$parsers.push(capitalize);
            capitalize(scope[attrs.ngModel]); // capitalize initial value
        }
    };
});

/***
GLobal Filters
***/

MetronicApp.filter('strReplace', function () {
    return function (input, from, to) {
        input = input || '';
        from = from || '';
        to = to || '';
        return input.replace(new RegExp(from, 'g'), to);
    };
});

MetronicApp.filter('replace', function () {
    return function (input, from, to) {
        if (input === undefined) {
            return;
        }

        var regex = new RegExp(from, 'g');
        return input.replace(regex, to);
    };

});

MetronicApp.filter('addressFilter', function () {
    return function (input) {
        if (input === undefined) {
            return;
        }

        var address = input.replace(/[ ,]/g, '');

        if (address === "") {
            return "";
        }
        else {
            return input;
        }
    };
});

MetronicApp.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            element.on('keypress', function (event, window) {
                var e = event || window.event;  // get event object
                var key = e.keyCode || e.which; // get key cross-browser

                if (key === 32) {
                    if (e.preventDefault()) {
                        e.preventDefault();
                    }
                    else {
                        e.returnValue = false;
                    }
                }
            });

            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('validNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) return;
            var range = attrs.validNumber.split(',').map(Number);
            ctrl.$validators.validNumber = function (value) {
                if (value != "") {
                    return value >= range[0] && value <= range[1];
                }
                else {
                    return true;
                }
            };
        }
    };
});

MetronicApp.directive('numbersDecimalOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            element.on('keypress', function (event, window) {
                var e = event || window.event;  // get event object
                var key = e.keyCode || e.which; // get key cross-browser

                if (key === 32) {
                    if (e.preventDefault()) {
                        e.preventDefault();
                    }
                    else {
                        e.returnValue = false;
                    }
                }
            });

            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9.]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('numbersComaOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            element.on('keypress', function (event, window) {
                var e = event || window.event;  // get event object
                var key = e.keyCode || e.which; // get key cross-browser

                if (key === 32) {
                    if (e.preventDefault()) {
                        e.preventDefault();
                    }
                    else {
                        e.returnValue = false;
                    }
                }
            });

            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9,]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('alphanumericOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9A-Za-zñÑ,\s]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('alphanumericDashOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9A-Za-z-ñÑ,\s]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('alphaDashOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^A-Za-z-ñÑ,\s,.]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('alphaSlashDashOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9A-Za-z-ñÑ@#$&(),/\s]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('forEmail', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            element.on('keypress', function (event, window) {
                var e = event || window.event;  // get event object
                var key = e.keyCode || e.which; // get key cross-browser

                if (key === 32) {
                    if (e.preventDefault()) {
                        e.preventDefault();
                    }
                    else {
                        e.returnValue = false;
                    }
                }
            });

            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9A-Za-z-ñÑ_.@-]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('validEmail', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, control) {
            control.$parsers.push(function (viewValue) {
                var newEmail = control.$viewValue;
                control.$setValidity("invalidEmail", true);
                if (typeof newEmail === "object" || newEmail == "") return newEmail;  // pass through if we clicked date from popup
                if (!newEmail.match(/^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;,.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$/))
                    control.$setValidity("invalidEmail", false);
                return viewValue;
            });
        }
    };
});

MetronicApp.directive('forBirthdate', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            element.on('keypress', function (event, window) {
                var e = event || window.event;  // get event object
                var key = e.keyCode || e.which; // get key cross-browser

                if (key === 32) {
                    if (e.preventDefault()) {
                        e.preventDefault();
                    }
                    else {
                        e.returnValue = false;
                    }
                }
            });

            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9\/]/ig, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('decimal', ['$filter', function ($filter) {
    return {
        require: '?ngModel',
        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) return;

            ctrl.$formatters.unshift(function (a) {
                return $filter(attrs.decimal)(ctrl.$modelValue, '')
            });

            elem.bind('blur', function (event) {
                var plainNumber = elem.val().replace(/[^\d|\-+|\.+]/g, '');
                elem.val($filter(attrs.decimal)(plainNumber, ''));
            });
        }
    };
}]);

MetronicApp.directive('currencyInput', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            attrs.$set('ngTrim', "false");
            $(element).css({ 'text-align': 'right' });

            var formatter = function (str, isNum) {
                str = String(Number(str || 0) / (isNum ? 1 : 100));
                str = (str == '0' ? '0.0' : str).split('.');
                str[1] = str[1] || '0';
                return str[0].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,') + '.' + (str[1].length == 1 ? str[1] + '0' : str[1]);
            }
            var updateView = function (val) {
                scope.$applyAsync(function () {
                    ngModel.$setViewValue(val || '');
                    ngModel.$render();
                });
            }
            var parseNumber = function (val) {
                var modelString = formatter(ngModel.$modelValue, true);
                var sign = {
                    pos: /[+]/.test(val),
                    neg: /[-]/.test(val)
                }
                sign.has = sign.pos || sign.neg;
                sign.both = sign.pos && sign.neg;

                console.log(attrs['currencyAllowedZero'])
                var zero = attrs['currencyAllowedZero'] != undefined && attrs['currencyAllowedZero'] == "true" ? "0.00" : "";
                console.log("||")
                console.log(!val || sign.has && val.length == 1 || ngModel.$modelValue && Number(val) === 0)
                if (!val || sign.has && val.length == 1 || ngModel.$modelValue && Number(val) === 0) {
                    var newVal = (!val || ngModel.$modelValue && Number() === 0 ? zero : val);
                    if (ngModel.$modelValue !== newVal)
                        updateView(newVal);

                    updateView(zero);
                }
                else {
                    var valString = String(val || '');
                    var newSign = (sign.both && ngModel.$modelValue >= 0 || !sign.both && sign.neg ? '-' : '');
                    var newVal = valString.replace(/[^0-9]/g, '');
                    var viewVal = newSign + formatter(angular.copy(newVal));

                    if (modelString !== valString)
                        updateView(viewVal);

                    return (Number(newSign + newVal) / 100) || 0;
                }
            }
            var formatNumber = function (val) {
                if (val) {
                    var str = String(val).split('.');
                    str[1] = str[1] || '0';
                    val = str[0] + '.' + (str[1].length == 1 ? str[1] + '0' : str[1]);
                }
                return parseNumber(val);
            }

            ngModel.$parsers.push(parseNumber);
            ngModel.$formatters.push(formatNumber);
        }
    };
});

MetronicApp.directive('percentageInput', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        scope: {
            // currencyIncludeDecimals: '&',
        },
        link: function (scope, element, attr, ngModel) {

            attr['percentageMaxValue'] = attr['percentageMaxValue'] || 100;
            attr['percentageMaxDecimals'] = attr['percentageMaxDecimals'] || 2;

            $(element).css({ 'text-align': 'right' });

            // function called when parsing the inputted url
            // this validation may not be rfc compliant, but is more
            // designed to catch common url input issues.
            function into(input) {

                var valid;

                if (input == '') {
                    ngModel.$setValidity('valid', true);
                    return '0.00';
                }

                // if the user enters something that's not even remotely a number, reject it
                if (!input.match(/^\d+(\.\d+){0,1}%{0,1}$/gi)) {
                    ngModel.$setValidity('valid', false);
                    return '0.00';
                }

                // strip everything but numbers from the input
                input = input.replace(/[^0-9\.]/gi, '');

                input = parseFloat(input);

                var power = Math.pow(10, attr['percentageMaxDecimals']);

                input = Math.round(input * power) / power;

                if (input > attr['percentageMaxValue']) input = attr['percentageMaxValue'];

                // valid!
                ngModel.$setValidity('valid', true);

                return parseFloat(input).toFixed(2);
            }

            ngModel.$parsers.push(into);

            function out(input) {
                if (ngModel.$valid && input !== undefined && input > '') {
                    return parseFloat(input).toFixed(2);
                }

                return '0.00';
            }

            ngModel.$formatters.push(out);

            $(element).bind('click', function () {
                //$( element ).val( ngModel.$modelValue );
                $(element).select();
            });

            $(element).bind('blur', function () {
                $(element).val(out(ngModel.$modelValue));
            });
        }
    };
});

MetronicApp.directive('setRequestDocumentTagsTo', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            var counter = 0;
            $('select.tags').each(function () {
                if (scope.requestDocumentTypeTagTos != undefined && counter < scope.requestDocumentTypeTagTos.length) {
                    $(this).val(scope.requestDocumentTypeTagTos[counter]);
                    counter++;
                }
            });
        }
    };
});

MetronicApp.directive('forBirthdateFormat', function () {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs, ctrl) {
            elm.on('keydown', function (event) {
                var $input = $(this);
                var value = $input.val();
                var inputLength = value.length;
                value = value.replace(/[^0-9\/]/ig, '');
                $input.val(value);

                if (inputLength === 2 || inputLength === 5) {
                    $input.val(value + "/");
                }

                if (event.which === 8 && (inputLength === 3 || inputLength === 5)) {
                    $input.val(value.substring(0, value.length - 1));
                }

                if (event.which === 64 || event.which === 16) {
                    // to allow numbers  
                    return false;
                } else if (event.which >= 48 && event.which <= 57) {
                    // to allow numbers  
                    return true;
                } else if (event.which >= 96 && event.which <= 105) {
                    // to allow numpad number  
                    return true;
                } else if ([8, 9, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                    // to allow backspace, enter, escape, arrows  
                    return true;
                } else {
                    event.preventDefault();
                    // to stop others  
                    //alert("Sorry Only Numbers Allowed");  
                    return false;
                }
            });
        }
    };
});

MetronicApp.directive('forTimeFormat', function () {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs, ctrl) {
            elm.on('keydown', function (event) {
                var $input = $(this);
                var value = $input.val();
                var inputLength = value.length;
                value = value.replace(/[^0-9: pam]/ig, '');
                $input.val(value);

                if (inputLength === 2) {
                    $input.val(value + ":");
                }

                if (event.which === 8 && (inputLength === 3 || inputLength === 5)) {
                    $input.val(value.substring(0, value.length - 1));
                }

                if (event.which === 64 || event.which === 16) {
                    // to allow numbers  
                    return false;
                } else if (event.which >= 48 && event.which <= 57) {
                    // to allow numbers  
                    return true;
                } else if (event.which >= 96 && event.which <= 105) {
                    // to allow numpad number  
                    return true;
                } else if ([8, 9, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                    // to allow backspace, enter, escape, arrows  
                    return true;
                } else {
                    event.preventDefault();
                    // to stop others  
                    //alert("Sorry Only Numbers Allowed");  
                    return false;
                }
            });
        }
    };
});

MetronicApp.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                if (changeEvent.target.files[0] !== undefined) {
                    var reader = new FileReader();
                    reader.onload = function (loadEvent) {
                        scope.$apply(function () {
                            scope.fileread = {};
                            scope.fileread.data = loadEvent.target.result.split(",")[1];
                            scope.fileread.name = changeEvent.target.files[0].name;
                            scope.fileread.size = changeEvent.target.files[0].size;
                            scope.fileread.type = changeEvent.target.files[0].type;
                        });
                    };
                    reader.readAsDataURL(changeEvent.target.files[0]);
                }
            });
        }
    };
}]);

MetronicApp.directive("filereadinfo", [function () {
    return {
        scope: {
            filereadinfo: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                scope.$apply(function () {
                    scope.filereadinfo = changeEvent.target.files[0];
                    scope.filereadinfo.data = changeEvent.target.files[0];
                    // or all selected files:
                    // scope.fileread = changeEvent.target.files;
                });
            });
        }
    };
}]);

MetronicApp.directive('alphanumericSlashDashOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9A-Za-z-ñÑ,\s\/]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('forWebsite', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9A-Za-z-ñÑ.\s\/]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return "";
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});

MetronicApp.directive('focusOn', function ($timeout) {
    return {
        restrict: 'A',
        link: function ($scope, $element, $attr) {
            $scope.$watch($attr.focusOn, function (_focusVal) {
                $timeout(function () {
                    _focusVal ? $element[0].focus() :
                        $element[0].blur();
                });
            });
        }
    };
});

MetronicApp.directive('masked', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, elm, attrs, ngModelCtrl) {
            elm.mask(attrs.format, {
                completed: function () {
                    ngModelCtrl.$setViewValue(elm.val());
                    scope.$apply();
                }
            });
        }
    };
}]);

MetronicApp.directive('autoFocus', function ($timeout) {
    return {
        link: function (scope, element, attrs) {
            attrs.$observe("autoFocus", function (newValue) {
                if (newValue === "true")
                    $timeout(function () { element.focus() });
            });
        }
    };
});

MetronicApp.directive('capitalize', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            var capitalize = function (inputValue) {
                if (inputValue == undefined) inputValue = '';
                var capitalized = inputValue.toUpperCase();
                if (capitalized !== inputValue) {
                    // see where the cursor is before the update so that we can set it back
                    var selection = element[0].selectionStart;
                    modelCtrl.$setViewValue(capitalized);
                    modelCtrl.$render();
                    // set back the cursor after rendering
                    element[0].selectionStart = selection;
                    element[0].selectionEnd = selection;
                }
                return capitalized;
            }
            modelCtrl.$parsers.push(capitalize);
            capitalize(scope[attrs.ngModel]); // capitalize initial value
        }
    };
});
