
angular.module("MetronicApp").factory('CustomService', CustomService);

function CustomService() {
    var customServices = {
        clearInputText: clearInputText
    };
    return customServices;

    function clearInputText(elementId, inputType) {

        var element = angular.element(document.querySelector(elementId));

        element.find(':input').each(function () {

            switch (inputType) {

                case 'text':
                    $(this).val('');
                case 'password':
                    $(this).val('');
            }
        });
    }
}