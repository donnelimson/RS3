angular.module('MetronicApp')
    .controller('NotificationController',
        ['$scope', 'NotificationService', '$window', 'CommonService','$timeout',
            function ($scope, NotificationService, $window, CommonService,$timeout) {

                $scope.userName = angular.element("#userName").val();
                $scope.unreadCount = null;

                this.$onInit = function () {
                    $scope.sendNotification();
                };

                $scope.sendNotification = function () {
                    NotificationService.GetNotifications({
                    }).then(function (data) {
                        $scope.setNotification(data.data)
                    });
                }

                $scope.setNotification = function (data,isTrigger) {
                    var approvalCount = document.getElementById("Count_Approval");
                    var hwiCount = document.getElementById("Count_Hwi");
                    var forConnectionsCount = document.getElementById("Count_ForConnections");
                    var issueForConnectionsCount = document.getElementById("Count_IssueForConnections");
                    var forDisconnectionsCount = document.getElementById("Count_ForDisconnections");
                    var processJobOrderCount = document.getElementById("Count_ProcessJobOrders");
                    var forAssignedJobOrderCount = document.getElementById("Count_ForAssignedJobOrders");

                    if (data != null) {
                        $scope.Notifications = data.Notifications;
                        $scope.unreadCount = data.UnreadCount;

                        if (approvalCount != null) {
                            approvalCount.innerHTML = data.PendingApprovalCount > 0 ? data.PendingApprovalCount : "";
                        }
                        if (hwiCount != null) {
                            hwiCount.innerHTML = data.PendingHwiCount > 0 ? data.PendingHwiCount : "";
                        }
                        if (forConnectionsCount != null) {
                            forConnectionsCount.innerHTML = data.PendingForConnectionCount > 0 ? data.PendingForConnectionCount : "";
                        }
                        if (issueForConnectionsCount != null) {
                            issueForConnectionsCount.innerHTML = data.PendingIssueForConnectionCount > 0 ? data.PendingIssueForConnectionCount : "";
                        }
                        if (forDisconnectionsCount != null) {
                            forDisconnectionsCount.innerHTML = data.PendingForDisconnectionsCount > 0 ? data.PendingForDisconnectionsCount : "";
                        }
                        if (processJobOrderCount != null) {
                            processJobOrderCount.innerHTML = data.PendingProcessJobOrderCount > 0 ? data.PendingProcessJobOrderCount : "";
                        }
                        if (forAssignedJobOrderCount != null) {
                            forAssignedJobOrderCount.innerHTML = data.PendingForAssignedJobOrderCount > 0 ? data.PendingForAssignedJobOrderCount : "";
                        }

                        if (data.Message != "" && data.Notification.MakerUserName != $scope.userName) {
                            //CommonService.infoMessage(data.Message);
                            toastr.options = {
                                "positionClass": "toast-bottom-right",
                                "closeButton": true,
                            }

                            toastr.options.onclick = function () {
                                $scope.process(data.Notification)
                            };

                            toastr["info"](data.Message, "System Message");
                        }
                    }
                    else {
                        $scope.Notifications = [];
                        $scope.unreadCount = null;

                        if (approvalCount != null) {
                            approvalCount.innerHTML = data.PendingApprovalCount > 0 ? data.PendingApprovalCount : "";
                        }
                        if (hwiCount != null) {
                            hwiCount.innerHTML = data.PendingHwiCount > 0 ? data.PendingHwiCount : "";
                        }
                        if (forConnectionsCount != null) {
                            forConnectionsCount.innerHTML = data.PendingForConnectionCount > 0 ? data.PendingForConnectionCount : "";
                        }
                        if (issueForConnectionsCount != null) {
                            forConnectionsCount.innerHTML = data.PendingIssueForConnectionCount > 0 ? data.PendingIssueForConnectionCount : "";
                        }
                        if (forDisconnectionsCount != null) {
                            forDisconnectionsCount.innerHTML = data.PendingForDisconnectionsCount > 0 ? data.PendingForDisconnectionsCount : "";
                        }
                        if (processJobOrderCount != null) {
                            processJobOrderCount.innerHTML = data.PendingProcessJobOrderCount > 0 ? data.PendingProcessJobOrderCount : "";
                        }
                    }
                }

                $scope.readNotification = function () {
                    NotificationService.MarkNotificationAllAsRead({
                    }).then(function (data) {
                        if (data.success) {
                            data.data = data.data == "" ? null : data.data;
                            $scope.setNotification(data.data)
                        }
                    });
                }

                $scope.process = function (notif) {
                    if (!notif.IsProcessed) {
                        NotificationService.MarkAsRead({ id: notif.NotificationDetailId }).then(function (data) {
                            if (data.success) {
                                $scope.setNotification(data.data);
                                if (notif.Destination == "Approval") {
                                    $window.location.href = document.Approval + "Process?id=" + notif.ReferenceId + "&approverId=" + notif.AppUserId+"&transaction="+notif.TransactionType;
                                }
                                if (notif.Destination == "House Wiring Inspection") {
                                    $window.location.href = document.CSAHouseWiringInspection + "ApproveHouseWiringInspection?houseWiringInspectionId=" + notif.ReferenceId;
                                }
                                if (notif.Destination == "House Wiring Inspection" && notif.Destination == "") {
                                    $window.location.href = document.CSAHouseWiringInspection + "ApprovedHouseWiringInspectionDetails?houseWiringInspectionId=" + notif.ReferenceId;
                                }
                            }
                        });
                    }
                    else {
                        if (notif.Destination == "Approval") {
                            $window.location.href = document.Approval + "Process?id=" + notif.ReferenceId + "&approverId=" + notif.AppUserId;
                        }
                        if (notif.Destination == "House Wiring Inspection") {
                            $window.location.href = document.CSAHouseWiringInspection + "ApproveHouseWiringInspection?houseWiringInspectionId=" + notif.ReferenceId;
                        }
                        if (notif.TransactionType == "Member Account Application" && notif.Destination == "") {
                            $window.location.href = document.CSAHouseWiringInspection + "ApprovedHouseWiringInspectionDetails?houseWiringInspectionId=" + notif.ReferenceId;
                        }
                    }
                };

                $(".toast toast-info").on('click', function (event) {
                    alert("wew")
                });
                //#region Rabbit MQ

                var pipe = function (el_name) {
                    var div = $(el_name);

                    var print = function (m, p) {
                        p = (p === undefined) ? '' : JSON.stringify(p);
                        div.append($("<code>").text(m + ' ' + p));
                        div.scrollTop(div.scrollTop() + 10000);
                    };

                    return print;
                };

                var client = Stomp.client('ws://127.0.0.1:15674/ws');
                //client.debug = pipe('#debug');

                var on_connect = function (x) {
                    id = client.subscribe($scope.userName, function (d) {
                        $scope.setNotification(angular.fromJson(d.body))
                        $scope.$apply();
                    });
           
                };

                var on_error = function () {
                    //console.log('error');
                };

                var test = client.connect('tarelco1', 'P@ssw0rd', on_connect, on_error, '/');
                //#endregion

            }]);