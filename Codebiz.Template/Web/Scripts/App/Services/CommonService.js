
angular.module("MetronicApp").factory('CommonService', ['$http', '$q','$window', function ($http, $q,$window) {
    return {
        showLoading: function () {
            //$("#formLoadingScreen").modal("show");
        },
        hideLoading: function () {
            //$("#formLoadingScreen").modal("hide");
        },
        showNavigating: function () {
            $("#NavigatingScreen").modal("show");
        },
        hideNavigating: function () {
            $("#NavigatingScreen").modal("hide");
        },
        infoMessage: function (message) {
            toastr.options = {"positionClass": "toast-bottom-right","closeButton": true,}
            toastr["info"](message, "System Message");
        },
        successMessage: function (message) {
            toastr.options = {"closeButton": true,}
            toastr["success"](message, "System Message");
        },
        errorMessage: function (error) {
            toastr.options = {"closeButton": true,}
            toastr["error"](error, "Error");
        },
        warningMessage: function (error) {
            toastr.options = {"closeButton": true,}
            toastr["warning"](error, "Warning");
        },
        notifMessage: function (notif, location) {
            var options = {
                positionClass: "toast-bottom-right",
                onclick: function () {
                    $window.location.href = location;
                }
            }
            toastr["info"](notif, "Notification", options);
        },
        confirmAction: function (callbackFunction) {
            swal({
                title: "Confirm action.",
                text: "Are you sure to proceed on this action?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#1ab394",
                confirmButtonText: "Yes proceed",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        recalculateTransaction: function (callbackFunction) {
            swal({
                title: "Re-calculate transaction?",
                text: "Transaction will be re-calculated",
                type: "info",
                showCancelButton: true,
                buttonColor: "#46b8da",
                confirmButtonText: "RE-CALCULATE",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        transferOrReceiveAction: function (callbackFunction, isTransfer) {
            swal({
                title: (isTransfer ? 'Transfer' : 'Receive').concat(" Record?"),
                text: "Record will be ".concat(isTransfer ? 'transferred' : 'received'),
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#1ab394",
                confirmButtonText: isTransfer ? "TRANSFER" : "RECEIVE",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        changeOfNameConfirmChanges: function (callbackFunction) {
            swal({
                title: "Update Account No?",
                text: "Consumer Details will be reset",
                type: "warning",
                confirmButtonText: "Update",
                cancelButtonText: "Cancel",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
            }, callbackFunction
            );
        },
        cancelChanges: function (callbackFunction) {
            swal({
                title: "Cancel Changes?",
                text: "Any Unsaved changes will be lost.",
                type: "warning",
                confirmButtonText: "Yes!",
                cancelButtonText: "No",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonClass: 'btn-danger',

            }, callbackFunction,

            );
        },
        cancelReconcilation: function (callbackFunction) {
            swal({
                title: "Cancel Reconcilation?",
                text: "This will cancel this reconcilation document. \nDo you want to Continue? .",
                type: "warning",
                confirmButtonText: "Yes!",
                cancelButtonText: "No",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonClass: 'btn-danger',

            }, callbackFunction,

            );
        },
        discardChanges: function (callbackFunction) {
            swal({
                title: "Discard Changes?",
                text: "Any Unsaved changes will be lost.",
                type: "warning",
                confirmButtonText: "Yes!",
                cancelButtonText: "No",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonClass: 'btn-danger',
            }, callbackFunction
            );
        },
        deActivate: function (callbackFunction, name) {
            swal({
                html: true,
                title: "Deactivate Record?",
                text: "<b>" + name + "</b>" + " will be deactivate",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
                confirmButtonText: "Deactivate",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        reActivate: function (callbackFunction, name) {
            swal({
                html: true,
                title: "Reactivate Record?",
                text: "<b>" + name + "</b>" + " will be reactivate",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
                confirmButtonText: "Reactivate",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        doneProcess: function (callbackFunction, name) {
            swal({
                html: true,
                title: "Finish Record?",
                text: "<b>" + name + "</b>" + " will be finished",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
                confirmButtonText: "Finish",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        saveChanges: function (callbackFunction) {
            swal({
                title: "Save Record?",
                text: "New record will be added",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Save",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        processChanges: function (callbackFunction) {
            swal({
                title: "Process Record?",
                text: "New record will be processed",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Process",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        processSPL: function (callbackFunction) {
            swal({
                title: "Process special lighting?",
                text: "Special lighting will be processed",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Process",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        postTransactions: function (callbackFunction) {
            swal({
                title: "Post Transactions?",
                text: "Are you sure to proceed on this action?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#1ab394",
                confirmButtonText: "Yes proceed",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        confirmPaymentOnAccount: function (message, callbackFunction) {
            swal({
                title: "Save Transactions?",
                text: `${message} of the Amount to be paid has no Matching Transaction.\nDo you want to Post this Amount as Payment On Account?`,
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#1ab394",
                confirmButtonText: "Yes proceed",
                closeOnConfirm: true
            }, callbackFunction)
        },
        withdraw: function (callbackFunction) {
            swal({
                title: "Are you sure to withdraw meter/s?",
                text: "This action is irreversible",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Save",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        reactivateDeactivate: function (callbackFunction, name, status) {
            var action = status ? "deactivate" : "reactivate";
            swal({
                html: true,
                title: action.charAt(0).toUpperCase() + action.slice(1) + " Record?",
                text: "<b>" + name + "</b>" + " will be " + action + "d",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
                confirmButtonText: action.charAt(0).toUpperCase() + action.slice(1),
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        evaluate: function (callbackFunction) {
            swal({
                title: "Evaluate Record?",
                text: "Coop evaluation will be added",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "EVALUATE",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        approvalOfRecord: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : "Save and Approve transaction?",
                text: hasApproval ? "Transaction will be recommended for approval" : "Transaction will be saved and approved.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Recommend" : "Save and Approved",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        bomApprovalOFRecord: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : "No Approval set up found, Bill Of Materials will be approved",
                text: hasApproval ? "Bill Of Materials will be recommended for approval" : "Bill Of Materials will be approved.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Recommend" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        specialLightingApprovalOfApplication: function (callbackFunction) {
            swal({
                title:  "Approve special lighting?",
                text:  "Special lighting will be approved.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText:  "APPROVE",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        materialApprovalOFRecord: function (callbackFunction, hasApproval, type) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : "No Approval set up found, " + type + " will be approved",
                text: hasApproval ? type + " will be recommended for approval" : type + " will be approved.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Recommend" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        approvalOfApplication: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : "No Approval set up found, application will be approved",
                text: hasApproval ? "Application will be recommended for approval" : "Application will be approved.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Recommend" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        approvalOfRecordProcess: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : "No Approval set up found, endorsed for processing?",
                text: hasApproval ? "Account will be recommended for approval" : "Account will be endorsed for processing",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Recommend" : "Endorse",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetup: function (callbackFunction, hasApproval, isBOM, title = "", text = "", confirmButtonText = "") {
            if (title == "") {
                title = hasApproval
                    ? "Recommend for Approval?"
                    : "No Approval set up found, automatically approved?";
            }

            if (text == "") {
                text = hasApproval
                    ? (!isBOM ? "Request" : "Bill of Materials") + " will be recommended for approval"
                    : (!isBOM ? "Request" : "Bill of Materials") + " will be saved and automatically approved";
            }

            if (confirmButtonText == "") {
                confirmButtonText = hasApproval ? "Recommend" : "Approve";
            }

            swal({
                title: title,
                text: text,
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: confirmButtonText,
                confirmButtonClass: 'btn-success',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        confimInfo: function (callbackFunction, title, text, confirmButtonText) {
            swal({
                title: title,
                text: text,
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: confirmButtonText,
                confirmButtonClass: 'btn-success',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        confirmInfo: function (callbackFunction, title, text, confirmButtonText) {
            swal({
                title: title,
                text: text,
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: confirmButtonText,
                cancelButtonText: "No",
                confirmButtonClass: 'btn-success',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForRequest: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Saved Request?" : "No Approval set up found, this request will be approved?",
                text: hasApproval ? "New request will be saved, subject for approval" : "Request will be approved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Save" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForApp: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Saved Application?" : "No Approval set up found, this application will be approved?",
                text: hasApproval ? "New application will be saved, subject for approval" : "Application will be approved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Save" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForAppSPL: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : "Save Record?",
                text: hasApproval ? "Record will be recommended for Approval." : "New record will be added.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "RECOMMEND" : "SAVE",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForInventoryTransferAndReceiving: function (callbackFunction, hasApproval, isTransfer) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : 'No Approval set up found, approve inventory '.concat(isTransfer ? 'transfer' : 'receiving').concat('?'),
                text: hasApproval ? "Inventory ".concat(isTransfer ? "Transfer" : "Receiving").concat(" will be recomended for approval") : "Inventory ".concat(isTransfer ? "Transfer" : "Receiving").concat(" will be approved"),
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Recommend" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForJobOrder: function (callbackFunction, hasApproval) {
            swal({
                title: "Save Job Order?",
                text: hasApproval ? "New job order will be saved, subject for approval." : "New job order will be saved.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Save",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForApplication: function (callbackFunction, hasApproval) {
            swal({
                title: "Save Discount Application?",
                text: hasApproval ? "New discount application will be saved, subject for approval" : "New discount application will be saved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Save",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForNetMetering: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Save Net Metering Application?" : "No Approval set up found, net metering will be approved?",
                text: hasApproval ? "New net metering will be save, subject for approval" : "Net metering will be saved as approved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Save" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForRV: function (callbackFunction, hasApproval, type) {
            swal({
                title: hasApproval ? "Save " + type + " Requisition Voucher?" : "No Approval set up found, " + type.toLowerCase() + " requisition voucher will be approved?",
                text: hasApproval ? "New " + type + " RV will be save, subject for approval" : type + " RV will be saved as approved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Save" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        checkIfHasSetupForMR: function (callbackFunction, hasApproval, isBOM) {
            swal({
                title: hasApproval ? "Save Material Request?" : "No Approval set up found,  Material request will be approved?",
                text: hasApproval ? "New material request will be save, subject for approval" : "Material request will be saved as approved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Save" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        HasPendingBurial: function (callbackFunction) {
            swal({
                title: "Account has pending approval for burial assistance, Do you want to proceed for change of name?",
                text: "The account will proceed for change of name",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Proceed",
                closeOnConfirm: true
            }, callbackFunction
            );
            return callbackFunction;
        },
        approvalOfVehicle: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Recommend for Approval?" : "No Approval set up found, endorse for records of travel?",
                text: hasApproval ? "Vehicle inspection will be recommend for approval" : "Vehicle inspection will be endorse for records of travel",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Recommend" : "Endorse",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        defaultAddressExist: function (callbackFunction) {
            swal({
                title: "Default address already exist! ",
                text: "would you like to replace it?",
                type: "warning",
                confirmButtonText: "Yes!",
                cancelButtonText: "No",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonClass: 'btn-danger'
            }, callbackFunction,
            );
        },
        deleteChanges: function (callbackFunction, name) {
            swal({
                html: true,
                title: "Delete Record?",
                text: "<b>" + name + "</b>" + " will be deleted",
                type: "error",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Delete",
                confirmButtonClass: 'btn-danger',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        CancelRequest: function (callbackFunction, name) {
            swal({
                html: true,
                title: "Cancel Record?",
                text: "<b>" + name + "</b>" + " will be cancelled",
                type: "error",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Cancel",
                confirmButtonClass: 'btn-danger',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        updateChanges: function (callbackFunction) {
            swal({
                title: "Update Record?",
                text: "Record will be updated",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Update",
                confirmButtonClass: 'btn-info',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        requestUpdateChanges: function (callbackFunction) {
            swal({
                title: "Update Request?",
                text: "Request will be updated",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Update",
                confirmButtonClass: 'btn-info',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        requestSaveChanges: function (callbackFunction) {
            swal({
                title: "Save Request?",
                text: "Request will be saved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Save",
                confirmButtonClass: 'btn-info',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        renewDiscountApplication: function (callbackFunction) {
            swal({
                title: "Renew discount application?",
                text: "Discount application will be renewed",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Renew",
                confirmButtonClass: 'btn-info',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        renew: function (callbackFunction) {
            swal({
                title: "Renew Record?",
                text: "Record be renewed",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "RENEW",
                confirmButtonClass: 'btn-info',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        verifyRecord: function (callbackFunction) {
            swal({
                title: "Verify Record?",
                text: "Record will be verified",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Verify",
                confirmButtonClass: 'btn-info',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        confimWarning: function (callbackFunction, title, text) {
            swal({
                title: title,
                text: text,
                type: "warning",
                confirmButtonText: "Yes!",
                cancelButtonText: "No",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonClass: 'btn-danger',
            }, callbackFunction
            );
        },
        confimInfo: function (callbackFunction, title, text, confirmButtonText) {
            swal({
                title: title,
                text: text,
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: confirmButtonText,
                confirmButtonClass: 'btn-success',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        messageInfo: function (callbackFunction, title, text, confirmButtonText, cancelButtonText, html = false) {
            swal({
                html: html,
                title: title,
                text: text,
                type: "info",
                showCancelButton: cancelButtonText=="" ? false:true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: confirmButtonText,
                confirmButtonClass: 'btn-success',
                cancelButtonText: cancelButtonText,
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        notificationWarning: function (callbackFunction, title, text, confirmButtonText) {
            swal({
                title: title,
                text: text,
                type: "warning",
                showCancelButton: false,
                confirmButtonColor: "#d9534f",
                confirmButtonText: confirmButtonText,
                confirmButtonClass: 'btn-success',
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        saveOrUpdateChanges: function (callbackFunction, id) {
            swal({
                title: id == 0 ? "Save Record?" : "Update Record",
                text: id == 0 ? "New record will be added" : "Record will be updated",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: id == 0 ? "Save" : "Update",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        terminateChanges: function (callbackFunction) {
            swal({
                title: "Terminate Record",
                text: "Record will be terminated",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "TERMINATE",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        rejectProcessApplication: function (callbackFunction) {
            swal({
                title: "Reject Record",
                text: "Record will be rejected.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "REJECT",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        saveOrUpdateUnits: function (callbackFunction) {
            swal({
                title:  "Update list of units?",
                text: "Units will be updated",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Update",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        saveOrUpdateRoutes: function (callbackFunction) {
            swal({
                title: "Update list of routes?",
                text: "Routes will be updated",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Update",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        endorseReendorse: function (callbackFunction, isEndorsed) {

            swal({
                title: !isEndorsed ? "Endorse account?" : "Re-endorse account?",
                text: !isEndorsed ? "Account will be endorsed for inspection." : "Account will be re-endorsed for inspection.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: !isEndorsed ? "Endorse" : "Re-endorse",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        endorseToIssueForConnection: function (callbackFunction, isEndorsed) {

            swal({
                title: !isEndorsed ? "Endorse account?" : "Re-endorse account?",
                text: !isEndorsed ? "Account will be endorsed to issue for connection." : "Account will be re-endorsed to issue for connection.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: !isEndorsed ? "Endorse" : "Re-endorse",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        supDocsConfirmation: function (callbackFunction, isComplete) {
            swal({
                title: "Update supporting document(s)?",
                text: isComplete ? "Are you sure you want update?" : "Requirements are incomplete, are you sure you want to update?",
                type: isComplete ? "info" : "warning",
                showCancelButton: true,
                confirmButtonClass: isComplete ? 'btn-primary' : 'btn-warning',
                confirmButtonText: "Update",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        saveApplicantConfirmation: function (callbackFunction, isUpdate, isComplete) {
            swal({
                title: isUpdate ? "Update record?" : "Save record?",
                text: isUpdate
                    ? isComplete
                        ? "Are you sure you want update?" : "Requirements are incomplete, are you sure you want to update?"
                    : isComplete
                        ? "Are you sure you want save?" : "Requirements are incomplete, are you sure you want to save?",
                type: isComplete ? "info" : "warning",
                showCancelButton: true,
                confirmButtonClass: isComplete ? 'btn-primary' : 'btn-warning',
                confirmButtonText: isUpdate ? "Update" : "Save",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        saveArrivalOrDeparture: function (callbackFunction, isDeparture) {
            swal({
                title: isDeparture == true ? "Save Departure?" : "Save Arrival",
                text: isDeparture == true ? "Departure record will be added" : "Arrival record will be added",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: "Save",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        confirmPayment: function (callbackFunction, totalPayment, totalAmountDue, openBalance, name, change, paymentOnAccount) {
            //text: "<font style='float:left;margin-left:70px'><b>Account No: </b></font><font style='float:right;margin-right:70px'>" + name + "</font>" + "<br>" + "<font style='float:left;margin-left:70px'><b>Payment On Account:</b> </font><font style='float:right;margin-right:70px'>" + (paymentOnAccount).toLocaleString(undefined, { minimumFractionDigits: 2, minimumFractionDigits: 2 }) + "</font>" + "<br>" + "<font style='float:left;margin-left:70px'><b>Amount Tendered: </b></font><font style='float:right;margin-right:70px'>" + (totalPayment).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + "</font>" + "<br>" + "<font style='float:left;margin-left:70px'><b>Amount Due: </b></font> <font style='float:right;margin-right:70px'>" + (totalAmountDue).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + "</font>" + "<br>" + "<font style='float:left;margin-left:70px'><b>Change: </b></font><font style='float:right;margin-right:70px;color:blue'><b>" + (change).toLocaleString(undefined, { minimumFractionDigits: 2, minimumFractionDigits: 2 }) + "</b></font>" + "<br>" + "<font style='float:left;margin-left:70px'><b>Open Balance: </b></font><font style='float:right;margin-right:70px'>" + (openBalance).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + "</font>" + "<br>",

            swal({
                html:true,
                title: "Confirm Payment",
                text: "<div style='height: 80px;'><font style='float:left;margin-left:70px'><b>Account No: </b></font><font style='float:right;margin-right:70px'>" + name + "</font>"  + "<br>" + "<font style='float:left;margin-left:70px'><b>Amount Tendered: </b></font><font style='float:right;margin-right:70px'>" + (totalPayment).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + "</font>" + "<br>" + "<font style='float:left;margin-left:70px'><b>Amount Due: </b></font> <font style='float:right;margin-right:70px'>" + (totalAmountDue).toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 }) + "</font>" + "<br>" + "<font style='float:left;margin-left:70px'><b>Change: </b></font><font style='float:right;margin-right:70px;color:blue'><b>" + (change).toLocaleString(undefined, { minimumFractionDigits: 2, minimumFractionDigits: 2 }) + "</b></font></div>",
                type: "info",
                confirmButtonText: "Save",
                cancelButtonText: "Cancel",
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
            }, callbackFunction
            );
        },
        checkIfHasSetupForWithdrawal: function (callbackFunction, hasApproval) {
            swal({
                title: hasApproval ? "Withdraw KWH Meter?" : "Automatically Approved?",
                text: hasApproval ? "KWH meter will be withdrawn, subject for approval" : "No Approval set up found, transaction will automatically approved",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonText: hasApproval ? "Save" : "Approve",
                closeOnConfirm: true
            }, callbackFunction
            );
        },
        PostData: function (data, url) {
            //var self = this;
            var d = $q.defer();
            $http({
                method: 'POST',
                url: url,
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

            return d.promise;
        },
        GetData: function (args, url, id) {
            var self = this;
            self.loadDiv(id);
            var d = $q.defer();
            $http({
                method: 'GET',
                url: url,
                params: args
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                    self.unloadDiv(id);
                }, function errorCallback(error) {
                    d.reject(error);
                    self.unloadDiv(id);
                });

            return d.promise;
        },
        loadDiv: function (id) {
            if (id !== null && id !== undefined) {
                App.blockUI({
                    target: '#' + id + "Div",
                    animate: true,
                });
            }
        },

        unloadDiv: function (id) {
            if (id !== null && id !== undefined) {
                window.setTimeout(function () {
                    App.unblockUI('#' + id + "Div");
                }, 500);
            }
        },

        //#region App User

        //RS3
        GetRolesForSelect2LookUp: function (args) {
            return this.GetData(args, document.Role + 'GetRolesForSelect2LookUp', null);
        },
        //




        GetAllPermissionsByPermissionGroupNames: function (args) {
            return this.GetData(args, document.AppUsers + 'GetAllPermissionsByPermissionGroupNames', null);
        },
        GetRoundingMethods: function (args, id) {
            return this.GetData(args, document.PriceList + 'GetRoundingMethods', id);
        },
        GetBasePriceList: function (args, id) {
            return this.GetData(args, document.PriceList + 'GetBasePriceLists', id);
        },
        GetAccountPhases: function (args, id) {
            return this.GetData(args, document.CSAMemberAccounts + 'GetAccountPhases', id);
        },
        SearchAppUserLookUp: function (args) {
            return this.GetData(args, document.AppUsers + 'SearchAppUserLookUp', null);
        },
        GetAppusersLookUp: function (args) {
            return this.GetData(args, document.AppUsers + 'GetAppusersLookUp', null);
        },
        SearchAppUsersModal: function (args) {
            return this.GetData(args, document.AppUsers + 'SearchAppUserModal', null);
        },
        GetAllPositions: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAllPositions', id);
        },
        GetAllAreas: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAllAreas', id);
        },
        GetAllUserGroups: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAllUserGroups', id);
        },
        GetAreaHighestPositionByOfficeId: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAreaHighestPositionByOfficeId', id);
        },
        GetCustomerGroupLookUp: function (args, id) {
            return this.GetData(args, document.CustomerGroup + 'GetCustomerGroupLookUp', id);
        },
        GetVendorGroupLookUp: function (args, id) {
            return this.GetData(args, document.BPVendorGroup + 'GetVendorGroupLookUp', id);
        },

        //#endregion

        //#region By Position
        GetMeterReaderLookUp: function (params, id) {
            return this.GetData(params, document.AppUsers + 'GetMeterReaderLookUp', id);
        },
        //#endregion

        // Management
        GetDepartmentLookUp: function (params, id) {
            return this.GetData(params, document.Department + 'GetDepartmentLookUp', id);
        },
        GetPositionLookUp: function (params, id) {
            return this.GetData(params, document.Position + 'GetPositionLookUp', id);
        },
        GetOfficesLookUp: function (params, id) {
            return this.GetData(params, document.Office + 'GetOfficesLookUp', id);
        },
        GetOfficesListForForward: function (params, id) {
            return this.GetData(params, document.Office + 'GetOfficesListForForward', id);
        },
        GetPolesLookUp: function (args) {
            return this.GetData(args, document.Pole + 'GetPolesLookUp', id);
        },
        GetAllUnitsAndKVARating: function (args) {
            return this.GetData(args, document.NoOfUnitsAndKvaRating + 'GetAllUnitsAndKVARating');
        },
        //#region Consumer Type
        GetConsumerTypes: function (args, id) {
            return this.GetData(args, document.ConsumerType + 'GetConsumerTypes', id);
        },
        GetConsumerTypesByMembershipType: function (args, id) {
            return this.GetData(args, document.ConsumerType + 'GetConsumerTypesByMembershipType', id);
        },
        GetConsumerTypeById: function (args, id) {
            return this.GetData(args, document.ConsumerType + 'GetConsumerTypeById', null);
        },
        GetConsumerTypeVoltage: function (args, id) {
            return this.GetData(args, document.ConsumerType + 'GetConsumerTypeVoltage', id);
        },
        GetDivisionCategories: function (params) {
            return this.GetData(params, document.Division + 'GetAllDivisionTypes');
        },
        GetConsumerClass: function (args, id) {
            return this.GetData(args, document.CSAHouseWiringInspection + 'GetConsumerClass', id);
        },
        GetDivisionCategoriesById: function (params, id) {
            return this.GetData(params, document.Division + 'GetAllDivisionTypesById', id);
        },
        GetByDivisionId: function (params, id) {
            return this.GetData(params, document.Division + 'GetByDivisionId', id);
        },

        //#endregion

        //#region Complaint Type
        GetCompliantTypes: function (args, id) {
            return this.GetData(args, document.CompliantType + 'GetCompliantTypes', id);
        },
        GetCompliantSubTypes: function (args, id) {
            return this.GetData(args, document.CompliantSubType + 'CompliantSubTypeIndex', id);
        },
        //#endregion

        //#region Address
        GetRegionLookUp: function (params, id) {
            return this.GetData(params, document.Region + 'GetRegionLookUp', id);
        },
        GetProvinceLookUp: function (params, id) {
            return this.GetData(params, document.Province + 'GetProvinceLookUp', id);
        },
        GetTarlacProvinceId: function (params, id) {
            return this.GetData(params, document.Province + 'GetTarlacProvinceId', id);
        },
        GetProvinceLookUpByRegionId: function (params, id) {
            return this.GetData(params, document.Province + 'GetProvinceByRegion', id);
        },
        GetCityTown: function (params, id) {
            return this.GetData(params, document.CityTown + 'GetCityTownLookUp', id);
        },
        GetBarangay: function (params, id) {
            return this.GetData(params, document.Barangay + 'GetBarangayLookUp', id);
        },
        GetRoute: function (params, id) {
            return this.GetData(params, document.Route + 'GetRouteLookUp', id);
        },
        GetRouteLookUpList: function (params, id) {
            return this.GetData(params, document.Route + 'GetRouteLookUpList', id);
        },
        
        GetPurok: function (params, id) {
            return this.GetData(params, document.Purok + 'GetPurokLookUp', id);
        },
        GetSitio: function (params, id) {
            return this.GetData(params, document.Sitio + 'GetSitioLookUp', id);
        },
        GetRouteBookNoById: function (params, id) {
            return this.GetData(params, document.Route + 'GetRouteBookNoById', id);
        },


        //#endregion

        GetPackagingTypeLookUp: function (params, id) {
            return this.GetData(params, document.PackagingType + 'GetPackagingTypeLookUp', id);
        },
        //GetShippingTypeLookUp: function (params, id) {
        //    return this.GetData(params, document.ShippingType + 'GetShippingTypeLookUp', id);
        //},
        GetUnitOfMeasureLookUp: function (params, id) {
            return this.GetData(params, document.UnitOfMeasure + 'GetPackagingTypeLookUp', id);
        },
        GetManucturerLookUp: function (params, id) {
            return this.GetData(params, document.Manufacturer + 'GetManucturerLookUp', id);
        },

        // CSA
        //#region Applicant
        GetMembershipTypes: function (args, id) {
            return this.GetData(args, document.CSAApplicant + 'GetMembershipTypes', id);
        },
        GetMembershipTypeSubcategories: function (args, id) {
            return this.GetData(args, document.CSAApplicant + 'GetMembershipTypeSubcategories', id);
        },
        GetMemberStatuses: function (args, id) {
            return this.GetData(args, document.CSAApplicant + 'GetMemberStatuses', id);
        },
        GetOwnershipTypes: function (args, id) {
            return this.GetData(args, document.CSAApplicant + 'GetOwnershipTypes', id);
        },
        GetBillingSubscriptions: function (args, id) {
            return this.GetData(args, document.CSAApplicant + 'GetBillingSubscriptions', id);
        },
        //#endregion

        // Request
        GetAllTransactionTypeForSupportingDocument: function (args, id) {
            return this.GetData(args, document.SupportingDocuments + 'GetAllTransactionTypeForSupportingDocument', id);
        },
        GetRequestTransactionTypes: function (args, id) {
            return this.GetData(args, document.SupportingDocuments + 'GetRequestTransactionTypes', id);
        },
        GetRequestTransactionSubTypesByTransactionTypeId: function (args, id) {
            return this.GetData(args, document.SupportingDocuments + 'GetRequestTransactionSubTypesByTransactionTypeId', id);
        },
        GetPurposeByTransactionTypeId: function (args, id) {
            return this.GetData(args, document.Purpose + 'GetPurposeByTransactionTypeId', id);
        },
        //#region Discount Application
        GetDiscountTypes: function (args, id) {
            return this.GetData(args, document.CSADiscountApplication + 'GetDiscountTypes', id);
        },
        GetDocumentTypes: function (args, id) {
            return this.GetData(args, document.CSADiscountApplication + 'GetDocumentTypes', id);
        },
        GetStatuses: function (args, id) {
            return this.GetData(args, document.CSADiscountApplication + 'GetStatuses', id);
        },
        GetBrands: function (args, id) {
            return this.GetData(args, document.Brand + 'GetBrandLookup', id);
        },
        GetAllBillingUnbundledTransactionLookup: function (args, id) {
            return this.GetData(args, document.BillingUnbundledTransaction + 'GetAllBillingUnbundledTransactionLookup', id);
        },
        
        //#endregion

        GetRelationships: function (args, id) {
            return this.GetData(args, document.CSAChangeOfName + 'GetRelationships', id);
        },
        GetChangeOfNameTypes: function (args, id) {
            return this.GetData(args, document.CSAChangeOfName + 'GetChangeOfNameTypes', id);
        },
        GetTransactionTypes: function (args, id) {
            return this.GetData(args, document.CSAIssueForConnection + 'GetTransactionTypes', id);
        },
        GetTransactionTypesRelatedToAccount: function (args, id) {
            return this.GetData(args, document.CSAMemberAccounts + 'GetTransactionTypesRelatedToAccount', id);
        },
        GetPositionsByDepartmentId: function (args, id) {
            return this.GetData(args, document.Department + 'GetPositionsByDepartmentId', id);
        },
        GetDepartmentByOfficeId: function (args, id) {
            return this.GetData(args, document.Office + 'GetDepartmentByOfficeId', id);
        },
        GetDepartmentsByOfficeAndCurrentDepartmentId: function (args, id) {
            return this.GetData(args, document.Office + 'GetDepartmentsByOfficeAndCurrentDepartmentId', id);
        },
        GetAppUserWithHighestPositionByDepartmentId: function (args) {
            return this.GetData(args, document.Department + 'GetAppUserWithHighestPositionByDepartmentId');
        },
        GetHeadOfficerDetailsById: function (args) {
            return this.GetData(args, document.Employees + 'GetHeadOfficerDetailsById');
        },
        GetPositionByOfficeAndDepartmentId: function (args) {
            return this.GetData(args, document.Position + 'GetPositionByOfficeAndDepartmentId');
        },
        GetPositionsByDivisionId: function (args, id) {
            return this.GetData(args, document.Division + 'GetPositionsByDivisionId', id);
        },
        GetAppUsersByPositionId: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAppUsersByPositionId', id);
        },
        GetAppUsersByPositionsByDivision: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAppUsersByPositionsByDivision', id);
        },
        GetHeadOfficerByDivisionId: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetHeadOfficerByDivisionId', id);
        },
        GetEmployeeById: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetEmployeeById', id);
        },
        GetDivisions: function (args, id) {
            return this.GetData(args, document.Division + 'GetAll', id);
        },
        GetAllDivisionsByDepartment: function (args, id) {
            return this.GetData(args, document.Division + 'GetAllByDepartment', id);
        },
        GetSupportingDocumentsByTransactionTypeId: function (args) {
            return this.GetData(args, document.SupportingDocuments + 'GetSupportingDocumentsByTransactionTypeId');
        },
        GetRouteDetails: function (args) {
            return this.GetData(args, document.Route + 'GetRouteDetails');
        },
        GetSuppliers: function (args) {
            return this.GetData(args, document.CSAHouseWiringInspection + 'GetSuppliers');
        },
        GetDocumentNumbering: function (args, id) {
            return this.GetData(args, document.DocumentNumbering + 'GetDocNumberingByTransactionTypeId', id);
        },
        GenerateDocNumber: function (args, id) {
            return this.GetData(args, document.DocumentNumbering + 'GenereateDocNumber', id);
        },
        GetAllDivisionCategories: function (args, id) {
            return this.GetData(args, document.Division + 'GetAllDivisionCategories', id);
        },
        GetAllTellersLooKUp: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAllTellersLooKUp', id);
        },
        GetAllByDepartmentId: function (args, id) {
            return this.GetData(args, document.Division + 'GetAllByDepartmentId', id);
        },

        GetComSectionHeads: function (args, id) {
            return this.GetData(args, document.AppUsers + 'GetAllCOMSectionHeads', id);
        },

        //#region Currency

        GetRoundingLookUp: function (params, id) {
            return this.GetData(params, document.Currency + 'GetAllRoundings', id);
        },
        GetAllDecimals: function (params, id) {
            return this.GetData(params, document.Currency + 'GetAllDecimals', id);
        },
        GetAllISOCurrencyCodes: function (params, id) {
            return this.GetData(params, document.Currency + 'GetAllISOCurrencyCodes', id);
        },

        //#endregion
        //#region Country Codes
        GetAllCountryCode: function (params, id) {
            return this.GetData(params, document.FINBank + 'GetCountryCodes', id);
        },
        GetAllPaperTypes: function (params, id) {
            return this.GetData(params, document.FINHouseBankAccount + 'GetPaperTypes', id);
        },
        GetAllCountries: function (params, id) {
            return this.GetData(params, document.FINHouseBankAccount + 'GetCountries', id);
        },
        GetAllTemplateNames: function (params, id) {
            return this.GetData(params, document.FINHouseBankAccount + 'GetTemplateNames', id);
        },
        //#endregion

        //banks
        GetBanks: function (params, id) {
            return this.GetData(params, document.FINBank + 'GetBankLookupDTO', id);
        },

        //#region Vehicles
        GetVehicleStatusLookUp: function (args, id) {
            return this.GetData(args, document.VHCCoopVehicles + 'GetStatuses', id);
        },
        GetVehicleTypesLookUp: function (args, id) {
            return this.GetData(args, document.VHCVihicleType + 'GetVehicleTypesLookUp', id);
        },
        GetRestrictionCode: function (args, id) {
            return this.GetData(args, document.VHCDrivers + 'GetRestrictionCode', id);
        },
        // GL Account string
        GetGlAccountByAcctCode: function (args, id) {
            return this.GetData(args, document.baseUrlNoArea + 'api/FIN/Setup/GetGLAccountByAcctCode/{searcher}"', id);
        },
        //#region item group
        GetItemGroupLookUp: function (args, id) {
            return this.GetData(args, document.ItemGroup + 'GetItemGroupLookUp', id);
        },
        GetUnitOfMeasureLookUp: function (args, id) {
            return this.GetData(args, document.ItemMasterData + 'GetUnitOfMeasureLookUp', id);
        },

        // Manufacturer lookup
        GetManufacturerLookUp: function (args, id) {
            return this.GetData(args, document.Manufacturer + 'GetManufacturerLookUp', id);
        },
        // Shipping type lookup
        GetShippingTypeLookUp: function (args, id) {
            return this.GetData(args, document.ShippingTypes + 'GetShippingTypeLookUp', id);
        },

        // Tax Group Lookup
        GetTaxGroupLookUp: function (args, id) {
            return this.GetData(args, document.baseUrlNoArea + 'api/FIN/GetTaxGroupLookUp', id);
        },

        GetTaxGroupLookupByCategory: function (q, args, id) {
            return this.GetData(args, document.baseUrlNoArea + `api/FIN/Setup/TaxGroup/GetLookupByCategory/${q}`, id);
        },

        GetCountryLookUp: function (args, id) {
            return this.GetData(args, document.Country + 'GetCountryLookUp', id);
        },
        GetProjectLookUp: function (args, id) {
            return this.GetData(args, document.FinancialProjects + 'GetProjectLookUp', id);
        },
        GetIndustryLookUp: function (args, id) {
            return this.GetData(args, document.BPIndustry + 'GetIndustryLookUp', id);
        },
        GetPriceListLookUp: function (args, id) {
            return this.GetData(args, document.PriceList + 'GetPriceListLookUp', id);
        },
        GetCurrencyLookUp: function (args, id) {
            return this.GetData(args, document.Currency + 'GetCurrencyLookUp', id);
        },
        GetCounterSetupTerminalLookUp: function (args, id) {
            return this.GetData(args, document.CounterSetup + 'GetCounterSetupTerminalLookUp', id);
        },
        GetWithholdingTaxLookup: function (args, id) {
            return this.GetData(args, document.WithholdingTax + 'GetWithholdingTaxLookUp', id);
        },
        GetWarehouseListLookUp: function (args, id) {
            return this.GetData(args, document.Warehouse + 'GetWarehouseListLookUp', id);
        },
        GetPackageTypeLookUp: function (args, id) {
            return this.GetData(args, document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/GetPackageTypeLookup', id);
        },
        CheckIfAccountHasUnpaidInvoice: function (args) {
            return this.PostData(args, document.CSAMemberAccounts + 'CheckIfAccountHasUnpaidInvoice');
        },
        CheckIfAccountHasPendingBurial: function (args) {
            return this.PostData(args, document.CSAMemberAccounts + 'CheckIfAccountHasPendingBurial', null);
        },
        CheckIfAccountHasPendingRequest: function (args) {
            return this.PostData(args, document.CSAMemberAccounts + 'CheckIfAccountHasPendingRequest', null);
        },
        GetAllLengthAndWidthUoM: function (params, id) {
            return this.GetData(params, document.InvUnitOfMeasure + 'GetAllLengthAndWidthUoM', id);
        },
        GetAllWeightUoM: function (params, id) {
            return this.GetData(params, document.InvUnitOfMeasure + 'GetAllWeightUoM', id);
        },
        GetLengthWidthDefaultUoM: function (params, id) {
            return this.GetData(params, document.InvUnitOfMeasure + 'GetLengthWidthDefaultUoM', id);
        },
        GetBrandAndSupplierByTranSensformerId: function (args, id) {
            return this.GetData(args, document.CSAForInventory + 'GetBrandAndSupplierByTransformerId', id);
        },
        GetForInventoryDetails: function (args, id) {
            return this.GetData(args, document.CSAForInventory + 'GetForInventoryDetails', id);
        },
        GetBankShortCut: function (args) {
            return this.GetData(args, document.APIBank + 'GetBankShortCut/{searcher}');
        },
        //#endregion
        GetAllLengthAndWidthUoM: function (params, id) {
            return this.GetData(params, document.InvUnitOfMeasure + 'GetAllLengthAndWidthUoM', id);
        },
        GetMeterBySerialNo: function (data) {
            return this.GetData(data, document.baseUrlNoArea + 'api/Meter/GetMeterBySerialNo/{serialNo}', null);
        },
        GetAllUoM: function (params, id) {
            return this.GetData(params, document.ItemMaster + 'GetAllUoM', id);
        },
        GetAllBrands: function (params, id) {
            return this.GetData(params, document.Brand + 'GetAllBrands', id);
        },
    };
}]);