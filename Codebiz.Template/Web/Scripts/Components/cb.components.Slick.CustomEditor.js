(function ($) {

    $.extend(true, window, {
        "Slick": {
            "CustomEditors": {
                "LinkTextBoxEditor": LinkTextBoxEditor,
                "SelectListEditor" : SelectListEditor
            }
        }
    })

    function LinkTextBoxEditor(args) {
        var $input;
        var $inputElement;
        var defaultValue;
        var newValue
        var selectedValue
        var scope = this;
        var calendarOpen = false;
        this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER];
        this.init = function () {
            $inputElement = $(`<div class="input-icon right">
                                    <i id="OBJ1002" name="OBJ1002" class="fa fa-search"  style="color:#32D5C2 !important; margin-top:1px !important;"></i>
                                    <input id="GOBJ1001" type="text" name="GOBJ1001" class="editor-text" value="" />
                               </div>`);

            $inputElement.appendTo(args.container);
            $input = $(`input[name="GOBJ1001"]`).select()
            $input.focus().select();


            var linkType = ""

            if (args.column.field == "ItemCode")
                linkType = 'ITM'
            if (args.column.field == "WhsCode")
                linkType = 'WHS'


            $("i[name='OBJ1002']").on('click', function (e) {
                $q.all([self.openModal(linkType)]).then(function (d) {
                    if (d) {
                        if (linkType === 'ITM') {
                            console.log("test");
                            $input.val(d[0].data.ItemCode)
                            newValue = d[0].data.ItemCode
                            selectedValue = d[0].data
                        }

                        if (linkType === 'WHS') {
                            $input.val(d[0].data.WhsCode)
                            newValue = d[0].data.ItemCode
                            selectedValue = d[0].data
                        }
                    }
                })
            })
        };


        this.destroy = function () {
            $inputElement.remove();
        };
        this.focus = function () {
            $input.focus();
        };
        this.loadValue = function (item) {
            defaultValue = item[args.column.field];
            $input.val(defaultValue);
            $input[0].defaultValue = defaultValue;
            $input.select();
        };
        this.serializeValue = function () {
            return $input.val();
        };
        this.applyValue = function (item, state) {
            item[args.column.field] = state;

            if (args.column.field === 'ItemCode')
                if (selectedValue)
                    item['Dscription'] = selectedValue.ItemName
        };
        this.isValueChanged = function () {
            return ($input.val() != defaultValue);
        };
        this.validate = function () {
            return {
                valid: true,
                msg: null
            };
        };
        this.init();
    }

    function SelectListEditor(args) {
        var $input;
        var defaultValue;
        var scope = this;
        var calendarOpen = false;
        this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER];


        var populateSelect = function (select, dataSource, addBlank) {
            var index, len, newOption;
            if (addBlank) { select.appendChild(new Option('', '')); }

            if (dataSource) {
                dataSource.forEach(x => {
                    newOption = new Option(x['LookupName'], x['LookupCode']);
                    select.appendChild(newOption);
                })
            }
        };

        this.init = function () {
            $inputElement = $(`<div><select id="OBJ0001"><option value="">--Please select--</option></select></div>`);

            $inputElement.appendTo(args.container);

            $inputElement.focus().select();

            $input = $(`select[id="OBJ0001"]`).select();

            //populateSelect($input[0], $scope.vatGroupList)
            populateSelect($input[0], args.column.dataSource)

            $input.select2({ allowClear: true }).val(args.item[args.column.field]).trigger('change')
            //$input.val(args.item[args.column.field])
            //if (args.item[args.column.field]) {
            //    $input.select2('val', args.item[args.column.field])
            //}


            $inputElement.children('span.select2.select2-container.select2-container--bootstrap')
                .children('span.selection')
                .children('span.select2-selection.select2-selection--single')
                .attr('style', `background-color:#fff !important; border: none;!important; color:#555 !important; outline:0; width:100%; height:inherit; padding-top:1px ;padding-bottom:1px;padding-left:1px`)
        };
        this.destroy = function () {
            //$input.autocomplete("destroy");
            $input.select2('close');
            $input.select2('destroy');
            $inputElement.remove();
        };
        this.focus = function () {
            $input.focus();
        };
        this.loadValue = function (item) {
            defaultValue = item[args.column.field];
            $input.val(defaultValue);
            //$input[0].defaultValue = defaultValue;
            $input.select();
        };
        this.serializeValue = function () {
            return $input.val();
        };
        this.applyValue = function (item, state) {
            item[args.column.field] = state;
        };
        this.isValueChanged = function () {
            return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
        };
        this.validate = function () {
            return {
                valid: true,
                msg: null
            };
        };
        this.init();
    }
})(jQuery);