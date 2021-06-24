(function () {

    let app = angular.module('SLICKGRID.MODULE', [])
    app
        .factory('slick.cell.formatter', ['$filter', function ($filter) {
            return {
                DateFormatter: function (row, cell, value, columnDef, dataContext) {
                    let res = $filter('date')(value, 'MM/dd/yyyy')
                    return res
                },

                LinkFormatter: function (row, cell, value, columnDef, dataContext) {
                    //return `<a href= "#"><i class="fa fa-arrow-right"></i> ${value}</a>` ;
                    //return `<button class="btn btn-mini btn-warning" ng-click="onDrillDownClick()"></i> &nbsp;&nbsp;${value}</button>`;
                    //return `<i class="fa fa-arrow-right" onclick="alert('lol')"></i> &nbsp;&nbsp;${value}`;
                    return `<span onclick="onDrillDownClick(event, '${value}')"><i class="fa fa-arrow-right"></i>&nbsp;${value}</span>`;
                },

                NumberFormatter: function (row, cell, value, columnDef, dataContext) {
                    if (parseFloat(value) !== 0) {
                        let res = $filter('number')(parseFloat(value), 2)
                        return res
                    }
                    return ""
                },

                CurrencyFormatter: function (row, cell, value, columnDef, dataContext) {
                    if (parseFloat(value) !== 0) {
                        let res = $filter('number')(parseFloat(value), 2).toLocaleString('en')
                        return res;
                    }
                    return ""
                },

                GroupFormatter: function (row, cell, value, columnDef, dataContext) {
                    if (value == null || value == undefined || dataContext === undefined) { return ""; }

                    value = value.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
                    var spacer = "<span style='display:inline-block;height:1px;width:" + (15 * dataContext["indent"]) + "px'></span>";
                    if (dataContext._collapsed) {

                        //return spacer + " <span class='toggle expand'></span>&nbsp;" + value;
                        if (dataContext.postable === 'Y') {
                            return `${spacer}<span class='toggle expand'></span>&nbsp;${value}`;
                        }
                        if (dataContext.postable === 'N') {
                            return `<strong>${spacer}<span class='toggle expand'></span>&nbsp;${value}</strong>`;
                        }

                    } else {
                        //return spacer + " <span class='toggle collapse'></span>&nbsp;" + value;
                        //return `${spacer}<span class='toggle expand'></span>&nbsp;${value}`;
                        if (dataContext.Postable === 'Y') {
                            return `${spacer}<span class='toggle expand'></span>&nbsp;${value}`;
                        }
                        if (dataContext.Postable === 'N') {
                            return `<strong>${spacer}<span class='toggle expand'></span>&nbsp;${value}</strong>`;
                        }
                    }
                },

                COAFormatter: function (row, cell, value, columnDef, dataContext) {
                    let data = dataContext
                    if (data.postable === 'Y') {
                        return value
                    }
                    else
                        return `<strong>${value}</strong>`
                },
            }
        }])
        .factory('slick.total.formatter', ['$filter', function ($filter) {
            return {
                GroupTotalFormatter: function (totals, columnDef, grid) {
                    var val = totals.sum && totals.sum[columnDef.field];
                    if (val !== null) {
                        return `<strong> ${$filter('number')(parseFloat(val), '2')}</strong>`;
                    }
                    return "";
                }
            }
        }])
        .factory('slick.CustomEditor', ['$rootScope', '$filter', '$uibModal', '$q', 'CommonService', function ($rootScope, $filter, $uibModal, $q, $cs) {
            let self = this;
            let objType = '';

            self.openModal = function (linkType, searchText, modal_size) {
                return $uibModal.open({
                    templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${linkType}`,
                    controller: 'ChooseFromListCtrl',
                    controllerAs: 'ctrl',
                    windowClass: 'modal_style',
                    backdrop: 'static',
                    keyboard: false,
                    size: modal_size,
                    resolve: {
                        modalData: function () {
                            return {
                                'LookupType': `${linkType}`,
                                'SearchText': `${searchText}`,
                                'Title': 'Choose From List'
                            };
                        }
                    } // data passed to the controller
                }).result
                    .then(function (d) {
                        return {
                            "linkType": linkType,
                            "data": d
                        }
                    },
                        function () { })
            }

            return {
                SetObjectType: function (args) {
                    objType = args;
                },
                LinkTextBoxEditor: function (args) {
                    var $input;
                    var $inputElement;
                    var defaultValue;
                    var selectedValue
                    this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER];
                    this.init = function () {
                        $inputElement = $(`<div class="input-icon right">
                                            <i id="OBJ1002" name="OBJ1002" class="fa fa-search"  style="color:#32D5C2 !important; margin-top:1px !important;"></i>
                                            <input id="GOBJ1001" type="text" name="GOBJ1001" class="editor-text" value="" autocomplete="off" />
                                            </div>`);

                        $inputElement.appendTo(args.container);
                        $input = $(`input[name="GOBJ1001"]`).select()
                        $input.focus().select();

                        let linkType = ""

                        if (args.column.field === "ItemCode")
                            linkType = 'ITM'

                        if (args.column.field === "WhsCode")
                            linkType = 'WHS'

                        if (args.column.field === "AcctCode")
                            linkType = 'ACT'

                        if (args.column.field === "AcctName")
                            linkType = 'ACT'

                        if (args.column.field === "COGSAcct")
                            linkType = 'ACT'

                        $("i[name='OBJ1002']").on('click', function (e) {
                            $q.all([
                                self.openModal(linkType, '', 'md')
                            ]).then(function (d) {
                                if (d) {
                                    if (linkType === 'ITM') {
                                        if (d[0]) {
                                            $input.val(d[0].data.ItemCode)
                                            newValue = d[0].data.ItemCode
                                            selectedValue = d[0].data
                                        }
                                    }

                                    if (linkType === 'WHS') {
                                        if (d[0]) {
                                            $input.val(d[0].data.WhsName)
                                            newValue = d[0].data.WhsName
                                            selectedValue = d[0].data
                                        }
                                    }

                                    if (linkType === 'ACT') {
                                        if (d[0]) {
                                            $input.val(d[0].data.AcctCode)
                                            newValue = d[0].data.AcctCode
                                            selectedValue = d[0].data
                                        }
                                    }
                                }
                                args.grid.navigateNext();
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

                        switch (objType) {
                            case 'SIN':
                                $rootScope.$broadcast('onApplySINLinkTextBox', args, item, state, selectedValue);
                                break;
                            case 'SDN':
                                $rootScope.$broadcast('onApplySINLinkTextBox', args, item, state, selectedValue);
                                break;

                            case 'SQU':
                                $rootScope.$broadcast('onApplySINLinkTextBox', args, item, state, selectedValue);
                                break;

                            case 'SCR':
                                $rootScope.$broadcast('onApplySINLinkTextBox', args, item, state, selectedValue);
                                break;

                            case 'SDP':
                                $rootScope.$broadcast('onApplySINLinkTextBox', args, item, state, selectedValue);
                                break;

                            case 'SOR':
                                $rootScope.$broadcast('onApplySINLinkTextBox', args, item, state, selectedValue);
                                break;

                            case 'SRD':
                                $rootScope.$broadcast('onApplySINLinkTextBox', args, item, state, selectedValue);
                                break;


                            default:
                                if (args.column.field === 'ItemCode') {
                                    if (selectedValue) {
                                        item['Dscription'] = selectedValue.ItemName
                                    }
                                }
                                break;
                        }
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
                },
                SelectListEditor: function SelectListEditor(args) {
                    var $input;
                    var defaultValue;
                    this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER];
                    var populateSelect = function (select, dataSource, addBlank) {
                        var index, len, newOption;
                        if (addBlank) { select.appendChild(new Option('', '')); }

                        if (dataSource) {
                            if (args.column.name === 'UOM') {
                                dataSource.forEach(x => {
                                    newOption = new Option(x['LookupName'], x['LookupCode']);
                                    select.appendChild(newOption);
                                })
                            }
                            else {
                                dataSource.forEach(x => {
                                    newOption = new Option(x['LookupName'], x['LookupCode']);
                                    select.appendChild(newOption);
                                })
                            }
                        }
                    };

                    this.init = function () {
                        /*$inputElement = $(`<div><select id="item_0014" select2><option value="">--Please select--</option></select></div>`);*/
                        $inputElement = $(`<div><select id="slick_select_item" select2><option value="">--Please select--</option></select></div>`);
                        $inputElement.appendTo(args.container);
                        $inputElement.focus().select();
                        $input = $(`select[id="slick_select_item"]`).select();

                        populateSelect($input[0], args.column.dataSource)
                        $input.select2({ allowClear: false }).val(args.item[args.column.field]).trigger('change')

                        $inputElement.children('span.select2.select2-container.select2-container--bootstrap')
                            .children('span.selection')
                            .children('span.select2-selection.select2-selection--single')
                            .attr('style', `background-color:#fff !important; border: none;!important; color:#555 !important; outline:0; width:100%; height:inherit; padding-top:1px ;padding-bottom:1px;padding-left:1px`)
                    };
                    this.destroy = function () {
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
                        $input.select();
                    };
                    this.serializeValue = function () {
                        return $input.val();
                    };
                    this.applyValue = function (item, state) {
                        item[args.column.field] = state;
                        switch (objType) {
                            case 'SIN':
                                $rootScope.$emit('onUpdateColumnData', args);
                                break;
                        }
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
                },
                DateEditor: function DateEditor(args) {

                    var $input;
                    var defaultValue;
                    var scope = this;

                    this.init = function () {
                        $inputElement = $(`<div class="input-icon right">
                                <i id="dateIcon" name="dateIcon" class="glyphicon glyphicon-calendar" style="color:#32D5C2 !important; margin-top:1px !important;"></i>
                                <input id="dateId" name="dateId" maxlength="10" placeholder="__/__/____" type="text" class="editor-text" value="" />
                            </div>`).appendTo(args.container);

                        $input = $(`input[name="dateId"]`).select();

                        $input.focus().datepicker({
                            format: 'mm/dd/yyyy',
                            orientation: 'top',
                            autoclose: true,

                        })
                        $input.on('change', function (event) {

                        });

                        $input.bind("keydown", scope.handleKeyDown);

                        $("i[name='dateIcon']").on('click', function (e) {
                            $input.focus().select();
                        })

                    }
                    this.handleKeyDown = function (e) {
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
                    };
                    this.destroy = function () {
                        $inputElement.remove();
                        $('.datepicker').hide();
                    }
                    this.focus = function () {
                        $input.focus();
                        $('.datepicker').hide();
                    }
                    this.getValue = function () {
                        return $input.val();
                    }
                    this.setValue = function (val) {
                        $input.val(val);
                    }
                    this.loadValue = function (item) {
                        defaultValue = item[args.column.field] || "";
                        $input.val(defaultValue);
                        $input[0].defaultValue = defaultValue;
                        $input.select();
                    }
                    this.serializeValue = function () {
                        return $input.val();
                    }
                    this.applyValue = function (item, state) {
                        item[args.column.field] = state;
                    };
                    this.isValueChanged = function () {
                        return (!($input.val() === "" && defaultValue == null)) && ($input.val() != defaultValue);
                    }
                    this.validate = function () {
                        if (args.column.validator) {
                            var validationResults = args.column.validator($input.val(), $input);
                            if (!validationResults.valid) {
                                $input.val("");
                                $cs.warningMessage("Invalid Date!")
                                return validationResults;
                            }
                        }

                        return { valid: true, msg: null };
                    }
                    this.init();
                },
                TextEditor: function TextEditor(args) {
                    var $input;
                    var defaultValue;

                    this.init = function () {
                        $input = $("<INPUT type=text class='editor-text' />")
                            .appendTo(args.container)
                            .focus()
                            .select();

                        $input.bind("keydown", this.handleKeyDown);
                    };

                    this.destroy = function () {
                        $input.remove();
                    };

                    this.focus = function () {
                        $input.focus();
                    };

                    this.getValue = function () {
                        return $input.val();
                    };

                    this.setValue = function (val) {
                        $input.val(val);
                    };

                    this.loadValue = function (item) {
                        defaultValue = item[args.column.field] || "";
                        $input.val(defaultValue);
                        $input[0].defaultValue = defaultValue;
                        $input.select();
                    };

                    this.serializeValue = function () {
                        return $input.val();
                    };

                    this.applyValue = function (item, state) {
                        item[args.column.field] = state;
                    };

                    this.isValueChanged = function () {
                        return (!($input.val() === "" && defaultValue == null)) && ($input.val() != defaultValue);
                    };

                    this.validate = function () {
                        if (args.column.validator) {
                            var validationResults = args.column.validator($input.val(), args.item.IsMainRow);
                            if (!validationResults.valid) {
                                this.applyValue(args.item, $input.val());
                                return validationResults;
                            }
                        }

                        return {
                            valid: true,
                            msg: null
                        };
                    };

                    this.init();
                }
            }
        }])
        .factory('slick.CustomValidator', ['$filter', function ($filter) {

            return {
                RequiredFieldValidator: function (value) {
                    if (value == null || value == undefined || !value.length || value == "") {
                        return { valid: false, msg: "This is a required field" };
                    } else {
                        return { valid: true, msg: null };

                    }
                },

                RequiredFieldIntegerValidator: function (value) {
                    if (isNaN(value) || value == null || value == undefined || value.length <= 0) {
                        return { valid: false, msg: "This is a required field" };
                    }
                    else if (parseInt(value) <= 0) {
                        return {
                            valid: false,
                            msg: "Quantity can't be less than or equal to zero(0)."
                        };
                    }

                    return {
                        valid: true,
                        msg: ""
                    };
                },

                InvalidDateValidator: function (value) {
                    if (value != null && value != "") {

                        var thisyear = $filter('date')(new Date(), "dd/MM/yyyy").substring(6, 11) - 1;

                        var year = value.substring(6, 11);
                        var month = value.substring(0, 2);
                        var day = value.substring(3, 5);

                        if (month > 12 || day > 31 || year < thisyear) {
                            return { valid: false, msg: "Invalid Date" };
                        }
                        else {
                            return { valid: true, msg: null };
                        }
                    }

                    return {
                        valid: true,
                        msg: ""
                    };
                }
            }
        }])
}())

$(document).ready(function () {
    $("form").on('submit', function (e) {
        e.preventDefault();
    });
})


let onDrillDownClick = (e, i) => {
    $.get(`${document.baseUrlNoArea}api/FIN/ChartOfAccount/GetChartOfAccountByAcctCode/${i}`, (d, s) => {
        if (s === 'success') {
            window.open(`${document.baseUrlNoArea}FIN/Finance/ChartOfAccount`, '_blank');
        }
    })


    $.get(`${document.baseUrlNoArea}api/BP/BusinessPartner/GetBusinessPartnerByCode/${i}`, (d, s) => {
        if (s === 'success') {
            let data = d.DataResult

            if (data.CardType == 'C')
                window.location.href = `${document.baseUrlNoArea}BP/BusinessPartnerAccount/Customer#!/VendorDetail/Edit/${i}`

            if (data.CardType == 'S')
                window.location.href = `${document.baseUrlNoArea}BP/BusinessPartnerAccount/Vendor#!/VendorDetail/Edit/${i}`
        }
    })
}

let onTransIdDrillDownClick = (e, i) => {
    window.location.href = `${document.baseUrlNoArea}FIN/Finance/JournalEntry#!/JENDetail/${i}`
}
