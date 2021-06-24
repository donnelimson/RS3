/***
 * Contains basic SlickGrid editors.
 * @module Editors
 * @namespace Slick
 */

(function ($) {
  // register namespace
    $.extend(true, window, {
        "Slick": {
            "Editors": {
            "Text": TextEditor,
            "Integer": IntegerEditor,
            "Float": FloatEditor,
            "Date": DateEditor,
            "YesNoSelect": YesNoSelectEditor,
            "Checkbox": CheckboxEditor,
            "PercentComplete": PercentCompleteEditor,
            "LongText": LongTextEditor,
            "SelectCellEditor": SelectCellEditor,
            }
        }
    });

    function SelectCellEditor(args) {
        var $select;
        var defaultValue;
        var scope = this;

        this.init = function () {

            if (args.column.options) {
                opt_values = args.column.options.split(',');
            } else {
                opt_values = "yes,no".split(',');
            }
            option_str = ""
            for (i in opt_values) {
                v = opt_values[i];
                option_str += "<OPTION value='" + v + "'>" + v + "</OPTION>";
            }
            $select = $("<div><select id='slickselect'>" + option_str + "</select></div>");
            $select.appendTo(args.container);
            $select.focus().select();
            $input = $(`select[id="slickselect"]`).select();
            $input.select2({ allowClear: false }).val(args.item[args.column.field]).trigger('change')

            $select.children('span.select2.select2-container.select2-container--bootstrap')
                .children('span.selection')
                .children('span.select2-selection.select2-selection--single')
                .attr('style', `background-color:#fff !important; border: none;!important; color:#555 !important; outline:0; width:100%; height:20px; padding-top:1px ;padding-bottom:1px;padding-left:1px`)
        };

        this.destroy = function () {
            $select.remove();
        };

        this.focus = function () {
            $select.focus();
        };

        this.loadValue = function (item) {
            defaultValue = item[args.column.field];
            $select.val(defaultValue);
        };

        this.serializeValue = function () {
            if (args.column.options) {
                return $select.val();
            } else {
                return ($select.val() == "yes");
            }
        };

        this.applyValue = function (item, state) {
            item[args.column.field] = state;
        };

        this.isValueChanged = function () {
            return ($select.val() != defaultValue);
        };

        this.validate = function () {
            return {
                valid: true,
                msg: null
            };
        };

        this.init();
    }
    function TextEditor(args) {
        var $input;
        var defaultValue;
        var scope = this;

        this.init = function () {
          var navOnLR = args.grid.getOptions().editorCellNavOnLRKeys;
          $input = $("<INPUT type=text class='editor-text' />")
              .appendTo(args.container)
              .on("keydown.nav", navOnLR ? handleKeydownLRNav : handleKeydownLRNoNav)
              .focus()
              .select();
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
            var validationResults = args.column.validator($input.val());
            if (!validationResults.valid) {
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
    function IntegerEditor(args) {

        var $input;
        var defaultValue;
        var scope = this;

        this.init = function () {
            var navOnLR = args.grid.getOptions().editorCellNavOnLRKeys;
            this.keyCaptureList = [Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER, Slick.keyCode.TAB];
            $input = $("<INPUT type=text class='editor-text text-right' maxlength='9' />")
                .appendTo(args.container)
                .on("keydown.nav", navOnLR ? handleKeydownLRNav : handleKeydownLRNoNav)
                .focus()
                .select()
                .inputFilter(function (value) {
                    return /^\d*$/.test(value); 
                });
        };

        this.destroy = function () {
          $input.remove();
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
          return parseInt($input.val(), 10) || 0;
        };

        this.applyValue = function (item, state) {
            item[args.column.field] = state;
        };

        this.isValueChanged = function () {
          return (!($input.val() === "" && defaultValue == null)) && ($input.val() != defaultValue);
        };

            this.validate = function () {
                var available = args.grid.getData()[args.grid.getActiveCell().row].Available;
                if (available !== undefined) {
                    if (parseInt($input.val()) > available) {
                        return {
                            valid: false,
                            msg: "Quantity can't be greater than available"
                        };
                    }
                }
                // console.log(args.grid.getActiveCell())
                var qtyReceived = args.grid.getColumns()[args.grid.getActiveCell().cell].id;
                if (qtyReceived == "quantityR") {
                    return {
                        valid: true,
                        msg: null
                    };
                }
                if (isNaN($input.val()) || $input.val() == '0' || $input.val() == '') {
                    toastr["warning"]("Can't have 0 value", "Warning");
            return {
              valid: false,
              msg: "Can't have 0 value"
            };

          }

          if (args.column.validator) {
            var validationResults = args.column.validator($input.val());
            if (!validationResults.valid) {
              return validationResults;
            }
          }

          return {
            valid: true,
            msg: null
          };
        };
          (function ($) {
              $.fn.inputFilter = function (inputFilter) {
                  return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
                      if (inputFilter(this.value)) {
                          this.oldValue = this.value;
                          this.oldSelectionStart = this.selectionStart;
                          this.oldSelectionEnd = this.selectionEnd;
                      } else if (this.hasOwnProperty("oldValue")) {
                          this.value = this.oldValue;
                          this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                      } else {
                          this.value = "";
                      }
                  });
              };
          }(jQuery));

        this.init();
      }
    function FloatEditor(args) {
        var $input;
        var defaultValue;
        var scope = this;
        this.init = function () {
          var navOnLR = args.grid.getOptions().editorCellNavOnLRKeys;
            $input = $("<INPUT type=text class='editor-text  text-right' />")
          .appendTo(args.container)
          .on("keydown.nav", navOnLR ? handleKeydownLRNav : handleKeydownLRNoNav)      
          .focus()
          .select();
        };

        this.destroy = function () {
          $input.remove();
        };

        this.focus = function () {
          $input.focus();
        };

        function getDecimalPlaces() {
            // returns the number of fixed decimal places or null
            var rtn = args.column.editorFixedDecimalPlaces;
            if (typeof rtn == 'undefined') {
                rtn = FloatEditor.DefaultDecimalPlaces;
            }
            return (!rtn && rtn!==0 ? null : rtn);
        }

        this.loadValue = function (item) {
          defaultValue = item[args.column.field];

          var decPlaces = getDecimalPlaces();
          if (decPlaces !== null
          && (defaultValue || defaultValue===0)
          && defaultValue.toFixed) {
            defaultValue = defaultValue.toFixed(decPlaces);
          }

          $input.val(defaultValue);
          $input[0].defaultValue = defaultValue;
          $input.select();
        };

        this.serializeValue = function () {
          var rtn = parseFloat($input.val());
          if (FloatEditor.AllowEmptyValue) {
            if (!rtn && rtn !==0) { rtn = ''; }
          } else {
            rtn = rtn || 0;
          }

          var decPlaces = getDecimalPlaces();
          if (decPlaces !== null
          && (rtn || rtn===0)
          && rtn.toFixed) {
            rtn = parseFloat(rtn.toFixed(decPlaces));
          }

          return rtn;
        };

        this.applyValue = function (item, state) {
            item[args.column.field] = state.toFixed(2);

            if (args.column.category == 'totalForRecommend') {
                var total = 0;
                $('#obj01').html(getSumOfCollection(args.grid.getData().map(r => r.Amount * 1), total).toFixed(2));
            }
        };

        this.isValueChanged = function () {
          return (!($input.val() === "" && defaultValue == null)) && ($input.val() != defaultValue);
        };

        this.validate = function () {
          if (isNaN($input.val())) {
            return {
              valid: false,
              msg: "Please enter a valid number"
            };
          }

          if (args.column.validator) {
            var validationResults = args.column.validator($input.val());
            if (!validationResults.valid) {
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

    FloatEditor.DefaultDecimalPlaces = null;
    FloatEditor.AllowEmptyValue = false;

    function DateEditor(args) {
    var $input;
    var defaultValue;
    var scope = this;
    var calendarOpen = false;

    this.init = function () {
      $input = $("<INPUT type=text class='editor-text' />");
      $input.appendTo(args.container);
      $input.focus().select();
      $input.datepicker({
        showOn: "button",
        buttonImageOnly: true,
         beforeShow: function () {
          calendarOpen = true;
        },
        onClose: function () {
          calendarOpen = false;
        }
      });
      $input.width($input.width() - 18);
    };

    this.destroy = function () {
      $.datepicker.dpDiv.stop(true, true);
      $input.datepicker("hide");
      $input.datepicker("destroy");
      $input.remove();
    };

    this.show = function () {
      if (calendarOpen) {
        $.datepicker.dpDiv.stop(true, true).show();
      }
    };

    this.hide = function () {
      if (calendarOpen) {
        $.datepicker.dpDiv.stop(true, true).hide();
      }
    };

    this.position = function (position) {
      if (!calendarOpen) {
        return;
      }
      $.datepicker.dpDiv
          .css("top", position.top + 30)
          .css("left", position.left);
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
    };

    this.isValueChanged = function () {
      return (!($input.val() === "" && defaultValue == null)) && ($input.val() != defaultValue);
    };

    this.validate = function () {
      if (args.column.validator) {
        var validationResults = args.column.validator($input.val());
        if (!validationResults.valid) {
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
    function YesNoSelectEditor(args) {
    var $select;
    var defaultValue;
    var scope = this;

    this.init = function () {
      $select = $("<SELECT tabIndex='0' class='editor-yesno'><OPTION value='yes'>Yes</OPTION><OPTION value='no'>No</OPTION></SELECT>");
      $select.appendTo(args.container);
      $select.focus();
    };

    this.destroy = function () {
      $select.remove();
    };

    this.focus = function () {
      $select.focus();
    };

    this.loadValue = function (item) {
      $select.val((defaultValue = item[args.column.field]) ? "yes" : "no");
      $select.select();
    };

    this.serializeValue = function () {
      return ($select.val() == "yes");
    };

    this.applyValue = function (item, state) {
      item[args.column.field] = state;
    };

    this.isValueChanged = function () {
      return ($select.val() != defaultValue);
    };

    this.validate = function () {
      return {
        valid: true,
        msg: null
      };
    };

    this.init();
  }
    function CheckboxEditor(args) {
    var $select;
    var defaultValue;
    var scope = this;

    this.init = function () {
      $select = $("<INPUT type=checkbox value='true' class='editor-checkbox' hideFocus>");
      $select.appendTo(args.container);
      $select.focus();
    };

    this.destroy = function () {
      $select.remove();
    };

    this.focus = function () {
      $select.focus();
    };

    this.loadValue = function (item) {
      defaultValue = !!item[args.column.field];
      if (defaultValue) {
        $select.prop('checked', true);
      } else {
        $select.prop('checked', false);
      }
    };

    this.preClick = function () {
        $select.prop('checked', !$select.prop('checked'));
    };

    this.serializeValue = function () {
      return $select.prop('checked');
    };

    this.applyValue = function (item, state) {
      item[args.column.field] = state;
    };

    this.isValueChanged = function () {
      return (this.serializeValue() !== defaultValue);
    };

    this.validate = function () {
      return {
        valid: true,
        msg: null
      };
    };

    this.init();
  }
    function PercentCompleteEditor(args) {
    var $input, $picker;
    var defaultValue;
    var scope = this;

    this.init = function () {
      $input = $("<INPUT type=text class='editor-percentcomplete' />");
      $input.width($(args.container).innerWidth() - 25);
      $input.appendTo(args.container);

      $picker = $("<div class='editor-percentcomplete-picker' />").appendTo(args.container);
      $picker.append("<div class='editor-percentcomplete-helper'><div class='editor-percentcomplete-wrapper'><div class='editor-percentcomplete-slider' /><div class='editor-percentcomplete-buttons' /></div></div>");

      $picker.find(".editor-percentcomplete-buttons").append("<button val=0>Not started</button><br/><button val=50>In Progress</button><br/><button val=100>Complete</button>");

      $input.focus().select();

      $picker.find(".editor-percentcomplete-slider").slider({
        orientation: "vertical",
        range: "min",
        value: defaultValue,
        slide: function (event, ui) {
          $input.val(ui.value);
        }
      });

      $picker.find(".editor-percentcomplete-buttons button").on("click", function (e) {
        $input.val($(this).attr("val"));
        $picker.find(".editor-percentcomplete-slider").slider("value", $(this).attr("val"));
      });
    };

    this.destroy = function () {
      $input.remove();
      $picker.remove();
    };

    this.focus = function () {
      $input.focus();
    };

    this.loadValue = function (item) {
      $input.val(defaultValue = item[args.column.field]);
      $input.select();
    };

    this.serializeValue = function () {
      return parseInt($input.val(), 10) || 0;
    };

    this.applyValue = function (item, state) {
      item[args.column.field] = state;
    };

    this.isValueChanged = function () {
      return (!($input.val() === "" && defaultValue == null)) && ((parseInt($input.val(), 10) || 0) != defaultValue);
    };

    this.validate = function () {
      if (isNaN(parseInt($input.val(), 10))) {
        return {
          valid: false,
          msg: "Please enter a valid positive number"
        };
      }

      return {
        valid: true,
        msg: null
      };
    };

    this.init();
  }

    /*
    * An example of a "detached" editor.
    * The UI is added onto document BODY and .position(), .show() and .hide() are implemented.
    * KeyDown events are also handled to provide handling for Tab, Shift-Tab, Esc and Ctrl-Enter.
    */
    function LongTextEditor(args) {
    var $input, $wrapper;
    var defaultValue;
    var scope = this;

    this.init = function () {
      var $container = $("body");
      var navOnLR = args.grid.getOptions().editorCellNavOnLRKeys;

      $wrapper = $("<DIV style='z-index:10000;position:absolute;background:white;padding:5px;border:3px solid gray; -moz-border-radius:10px; border-radius:10px;'/>")
          .appendTo($container);

      $input = $("<TEXTAREA hidefocus rows=5 style='background:white;width:250px;height:80px;border:0;outline:0'>")
          .appendTo($wrapper);

      $("<DIV style='text-align:right'><BUTTON>Save</BUTTON><BUTTON>Cancel</BUTTON></DIV>")
          .appendTo($wrapper);

      $wrapper.find("button:first").on("click", this.save);
      $wrapper.find("button:last").on("click", this.cancel);
      $input.on("keydown", this.handleKeyDown); 

      scope.position(args.position);
      $input.focus().select();
    };

    this.handleKeyDown = function (e) {
      if (e.which == $.ui.keyCode.ENTER && e.ctrlKey) {
        scope.save();
      } else if (e.which == $.ui.keyCode.ESCAPE) {
        e.preventDefault();
        scope.cancel();
      } else if (e.which == $.ui.keyCode.TAB && e.shiftKey) {
        e.preventDefault();
        args.grid.navigatePrev();
      } else if (e.which == $.ui.keyCode.TAB) {
        e.preventDefault();
        args.grid.navigateNext();
      } else if (e.which == $.ui.keyCode.LEFT || e.which == $.ui.keyCode.RIGHT) {
        if (args.grid.getOptions().editorCellNavOnLRKeys) {
          var cursorPosition = this.selectionStart;
          var textLength = this.value.length;
          if (e.keyCode === $.ui.keyCode.LEFT && cursorPosition === 0) {
            args.grid.navigatePrev();
          }
          if (e.keyCode === $.ui.keyCode.RIGHT && cursorPosition >= textLength-1) {
            args.grid.navigateNext();
          }
        }
      }
    };

    this.save = function () {
      args.commitChanges();
    };

    this.cancel = function () {
      $input.val(defaultValue);
      args.cancelChanges();
    };

    this.hide = function () {
      $wrapper.hide();
    };

    this.show = function () {
      $wrapper.show();
    };

    this.position = function (position) {
      $wrapper
          .css("top", position.top - 5)
          .css("left", position.left - 5);
    };

    this.destroy = function () {
      $wrapper.remove();
    };

    this.focus = function () {
      $input.focus();
    };

    this.loadValue = function (item) {
      $input.val(defaultValue = item[args.column.field]);
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
        var validationResults = args.column.validator($input.val());
        if (!validationResults.valid) {
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
    /*
    * Depending on the value of Grid option 'editorCellNavOnLRKeys', us 
    * Navigate to the cell on the left if the cursor is at the beginning of the input string
    * and to the right cell if it's at the end. Otherwise, move the cursor within the text
    */
    function handleKeydownLRNav(e) {
    var cursorPosition = this.selectionStart;
    var textLength = this.value.length;
    if ((e.keyCode === $.ui.keyCode.LEFT && cursorPosition > 0) ||
         e.keyCode === $.ui.keyCode.RIGHT && cursorPosition < textLength-1) {
      e.stopImmediatePropagation();
    }
  }
    function handleKeydownLRNoNav(e) {
    if (e.keyCode === $.ui.keyCode.LEFT || e.keyCode === $.ui.keyCode.RIGHT) {	
      e.stopImmediatePropagation();	
    }	
    }
})(jQuery);
