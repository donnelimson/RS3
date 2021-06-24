angular.module('MetronicApp').controller('DocumentHistorycontroller',
    ['$scope', 'CommonService', '$window', 'ShareDataService','$uibModal',
        function ($scope, CommonService, $window, ShareDataService, $uibModal) {
            $scope.showDetails = function (id, transId) {
    
                switch (transId) {
                    case TRANSACTION_TYPE.Transformer_Rental.Value:
                        $window.open(document.CSATransformerRental
                            + "Details?id=" +id
                            + "&forView=" + true, '_blank');
                        break;
                    case TRANSACTION_TYPE.Contestable_Application.Value:
                        $window.open(document.CSAContestableApplication
                            + "Details?id=" + id
                            + "&forView=" + true, '_blank');
                        break;
                    case TRANSACTION_TYPE.Job_Order_Indirect.Value:
                        $window.open(document.JobOrderRequest
                            + "Details?jobOrderId=" + id
                            + "&forView=" + true, '_blank');
                        break;
                    case TRANSACTION_TYPE.Job_Order_Direct.Value:
                        $window.open(document.JobOrderRequest
                            + "Details?jobOrderId=" + id
                            + "&forView=" + true, '_blank');
                        break;

                    // Material Request
                    case TRANSACTION_TYPE.Material_Request.Value:
                        $window.open(document.MaterialRequest
                            + "Details?Id=" + id
                            + "&forView=" + true, '_blank');
                        break;
                    // Stock Requisition Voucher
                    case TRANSACTION_TYPE.Stock_Requisition_Voucher.Value:
                        $window.open(document.RequisitionVoucher + 'Stock#!/StockRequisitionVoucher/' + 'View/' + id + '/' + '-1' + '/' + true, '_blank');
                        break;
                    // Purchase Requisition Voucher
                    case TRANSACTION_TYPE.Purchase_Requisition_Voucher.Value:
                        $window.open(document.RequisitionVoucher + 'Purchase#!/PurchaseRequisitionVoucher/' + 'View/' + id + '/' + '-1'  + '/' + true, '_blank');
                        break;
                    //Changeof Meter
                    case TRANSACTION_TYPE.Change_of_Meter.Value:
                        $window.open(document.CSAChangeOfMeter
                            + "Details?id=" + id
                            + "&forView=" + true, '_blank');
                        break;
                    case TRANSACTION_TYPE.Approval.Value:
                        $uibModal.open({
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: '_ApprovalResultModal.html',
                            controller: 'ApprovalResultController',
                            size: 'xlg',
                            keyboard: false,
                            backdrop: "static",
                            windowClass: 'modal_dialog',
                            modalOverflow: true,
                            resolve: {
                                ApprovalId: function () {
                                    return id
                                }
                            }
                        }).result.then(function () {
                        }, function () {
                        });
                        break;
                    default:
                   
                    break;
                }
            }
         
        }]);