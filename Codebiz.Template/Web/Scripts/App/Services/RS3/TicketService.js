
angular.module("MetronicApp").
    factory('TicketService', ['CommonService', function (commonService) {
        return {
            Search: function (args) {
                return commonService.PostData(args, document.Ticket + 'Search', null);
            },
            AddOrUpdate: function (args) {
                return commonService.PostData(args, document.Ticket + 'AddOrUpdate', null);
            },
            GetTicketDetailsById: function (args) {
                return commonService.GetData(args, document.Ticket + 'GetTicketDetailsById', null);
            },
            SubmitComment: function (args) {
                return commonService.PostData(args, document.Ticket + 'SubmitComment', null);
            },
            ResolveOrReopenTicket: function (args) {
                return commonService.PostData(args, document.Ticket + 'ResolveOrReopenTicket', null);
            },
            TakeTicket: function (args) {
                return commonService.PostData(args, document.Ticket + 'TakeTicket', null);
            },
            CheckIfClient: function (args) {
                return commonService.GetData(args, document.Ticket + 'CheckIfClient', null);
            },
            
        }
    }]);