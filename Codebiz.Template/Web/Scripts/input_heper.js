$(document).ready(function () {
    input_helper.initialize();
});

input_helper = {
    initialize: function () {
        $('.capFirstChar').keydown(function (event) {
            var $t = $(this);
            if (this.selectionStart == 0 && event.keyCode >= 65 && event.keyCode <= 90 && !(event.shiftKey) && !(event.ctrlKey) && !(event.metaKey) && !(event.altKey)) {
                event.preventDefault();
                var char = String.fromCharCode(event.keyCode);
                $t.val(char + $t.val().slice(this.selectionEnd));
                this.setSelectionRange(1, 1);
            }
        });

        $('.noSpace').keyup(function (event) {
            var $t = $(this);
            if (input_helper.nospace($t)) {
                event.preventDefault();
            }
        });

        $('.integerOnly').keypress(function (e) {
            var keyCode = (e.which) ? e.which : e.keyCode
            if ($.inArray(keyCode, [8, 9, 27, 13, 32, 45]) !== -1 ||
                // Allow: Ctrl+A
                (keyCode == 65 && e.ctrlKey === true) ||
                // Allow: home, end, left, right, down, up
                (keyCode >= 35 && keyCode <= 40) ||
                e.altKey) {

                return;
            }

            if (e.keyCode < 48 || e.keyCode > 57) {
                return false;
            }

            return true;
        });

        $(".currency-numeric").keydown(function (e) {
            var keyCode = (e.which) ? e.which : e.keyCode
            if (e.target.value.indexOf('.') > -1 && (keyCode == 190 || keyCode == 110)) {
                return false;
            } else {
                if ($.inArray(keyCode, [46, 8, 9, 27, 13, 110, 32, 190]) !== -1 ||
                    // Allow: Ctrl+A
                    ((e.keyCode == 65 || e.keyCode == 86 || e.keyCode == 67) && (e.ctrlKey === true || e.metaKey === true))) {

                    return;
                }

                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }

                return true;
            }
        });

        $(".thousandSeparator").focus(function () {
            this.value = this.value.toString().replace(/,/g, "");
        });

        $(".thousandSeparator").focusout(function () {
            var val = input_helper.thousandSeparator(this.value);
            this.value = val;
        });
    },
    thousandSeparator: function (int) {
        var x = parseFloat(int).toFixed(2);
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    },
    nospace: function (data) {
        if (data.val().trim().length === 0) {
            data.val("");
        }

        if (data.val().trim().length > 0 && data.val().charCodeAt(0) == 32) {
            data.val(data.val().substr(1));
        }

        if ((data.val().trim().length === 0 && event.which === 32) ||
            (data.val().trim().length > 0 && data.val().charCodeAt(0) == 32)) {
            return true;
        }

        return false;
    },
}
