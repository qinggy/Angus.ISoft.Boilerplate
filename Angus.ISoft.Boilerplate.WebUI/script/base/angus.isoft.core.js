$.namespace('angus.isoft.core');
//
Array.prototype.indexOf = function (val) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == val) return i;
    }
    return -1;
}
//数组添加remove方法
Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
}

angus.isoft.core.Config = {
    APIBaseUrl: "",
    ajaxProcessingText: "加载中....",
    ajaxProcessedText: "完成"
}

angus.isoft.core.getJsonResult = function (url, successCallBack, failureCallBack) {
    GlobalLoader.ShowLoader();

    $.ajaxSetup({
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
        }
    });
    $.getJSON(url+"?_t="+ new Date().getTime() )
        .success(function (data) {
            GlobalLoader.HideLoader();
            if (data.Code != undefined && data.Code != null && data.Code == 401) {
                location.href = "/";
            }//“If-Modified-Since”,”0”

            successCallBack(data);

        })
        .error(function OnError(xhr, textStatus, err) {
            if (err == "Unauthorized") {
                location.reload();
            }
            if (failureCallBack != null) {
                var obj = jQuery.parseJSON(xhr.responseText);
                var errObj = new Object();
                errObj.Message = obj.Message;

                if (obj.ModelState != null)
                    errObj.ModelState = obj.ModelState;

                errObj.status = xhr.status;
                errObj.statusText = xhr.statusText;
                failureCallBack(errObj)
            }

        });

};

angus.isoft.core.getJsonResultRR = function (url, successCallBack, failureCallBack) {
    $.ajaxSetup({
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
        }
    });
    $.getJSON(url + "?_t=" + new Date().getTime())
        .success(function (data) {
            if (data.Code != undefined && data.Code != null && data.Code == 401) {
                location.href = "/";
            }//“If-Modified-Since”,”0”

            successCallBack(data);

        })
        .error(function OnError(xhr, textStatus, err) {
            if (err == "Unauthorized") {
                location.reload();
            }
            if (failureCallBack != null) {
                var obj = jQuery.parseJSON(xhr.responseText);
                var errObj = new Object();
                errObj.Message = obj.Message;

                if (obj.ModelState != null)
                    errObj.ModelState = obj.ModelState;

                errObj.status = xhr.status;
                errObj.statusText = xhr.statusText;
                failureCallBack(errObj)
            }

        });

};

angus.isoft.core.getJSONData = function (url, successCallBack, failureCallBack) {

    $.ajaxSetup({
        cache: false,
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
        }
    });
    $.getJSON(this.Config.APIBaseUrl + url)

    .success(function (data) {
        successCallBack(data);

    })
        .error(function OnError(xhr, textStatus, err) {

            if (failureCallBack != null) {
                var obj = jQuery.parseJSON(xhr.responseText);
                var errObj = new Object();
                errObj.Message = obj.Message;
                errObj.status = xhr.status;
                errObj.statusText = xhr.statusText;
                failureCallBack(errObj)

            };
        });
}

//Web api - Http get operation - Data fetch
angus.isoft.core.getJSONDataBySearchParam = function (url, object, successCallBack, failureCallBack, beforeSendCallBack, onCompleteCallBack) {
    $.ajax({
        url: this.Config.APIBaseUrl + url,
        cache: false,
        type: 'GET',
        data: object,
        beforeSend: beforeSendCallBack == undefined ? undefined : beforeSendCallBack,
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
            onCompleteCallBack;
        }
    })
     .success(function (data) { successCallBack(data); })
     .error(function OnError(xhr, textStatus, err) {
         if (failureCallBack != null) {
             var obj = jQuery.parseJSON(xhr.responseText);
             var errObj = new Object();
             errObj.Message = obj.Message;

             if (obj.ModelState != null)
                 errObj.ModelState = obj.ModelState;

             errObj.status = xhr.status;
             errObj.statusText = xhr.statusText;
             failureCallBack(errObj)
         }
     });
}

// Web api - Http put operation - record update
angus.isoft.core.doPutOperation = function (url, object, successCallBack, failureCallBack) {
    $.ajax({
        url: this.Config.APIBaseUrl + url,
        cache: false,
        type: 'PUT',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(object),
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
        }
    })
    .success(function (data) {
        successCallBack(data);
    })
    .error(function OnError(xhr, textStatus, err) {

        if (failureCallBack != null) {
            var obj = jQuery.parseJSON(xhr.responseText);
            var errObj = new Object();
            errObj.Message = obj.Message;

            if (obj.ModelState != null)
                errObj.ModelState = obj.ModelState;

            errObj.status = xhr.status;
            errObj.statusText = xhr.statusText;
            failureCallBack(errObj)
        }
    });
}

angus.isoft.core.doPost = function (url, obj, success, failure) {
    $.ajax({
        url: this.Config.APIBaseUrl + url,
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: obj,
        statusCode: {
            200: function (data) {
                success(data);
            }
        },
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
        }
    }).error(function OnError(xhr, textStatus, err) {

        if (failure != null) {
            var obj = jQuery.parseJSON(xhr.responseText);
            var errObj = new Object();
            errObj.Message = obj.Message;

            if (obj.ModelState != null)
                errObj.ModelState = obj.ModelState;

            errObj.status = xhr.status;
            errObj.statusText = xhr.statusText;
            failure(errObj)
        }
    });
}


// Web api - Http post operation - create record
angus.isoft.core.doPostOperation = function (url, object, successCallBack, failureCallBack) {
    //
    $.ajax({
        url: this.Config.APIBaseUrl + url,
        cache: false,
        type: 'POST',
        contentType: 'application/json',
        dataType: "json",
        data: JSON.stringify(object),
        statusCode: {
            200: function (data) {
                successCallBack(data)
            }
        },
        beforeSend: function () {
            GlobalLoader.ShowLoader();
        },
        complete: function (XMLHttpRequest, textStatus) {
            GlobalLoader.HideLoader();
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
            var unauthorize = XMLHttpRequest.getResponseHeader("authorize");
            if (unauthorize == "unauthorize") {
                location.href = "/UnAuthrize";
            }
        }

    })
      .error(function OnError(xhr, textStatus, err) {

          if (failureCallBack != null) {
              var obj = jQuery.parseJSON(xhr.responseText);
              var errObj = new Object();
              errObj.Message = obj.Message;

              if (obj.ModelState != null)
                  errObj.ModelState = obj.ModelState;

              errObj.status = xhr.status;
              errObj.statusText = xhr.statusText;
              failureCallBack(errObj)
          }
      });
}


// Web api - Http post operation - create record
angus.isoft.core.doPostLoad = function (url, object, successCallBack, failureCallBack) {
    //
    $.ajax({
        url: this.Config.APIBaseUrl + url,
        cache: false,
        type: 'POST',
        contentType: 'application/json',
        dataType: "json",
        data: object,
        statusCode: {
            200: function (data) {
                successCallBack(data)
            }
        },
        beforeSend: function () {
            GlobalLoader.ShowLoader();
        },
        complete: function (XMLHttpRequest, textStatus) {
            GlobalLoader.HideLoader();
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
            var unauthorize = XMLHttpRequest.getResponseHeader("authorize");
            if (unauthorize == "unauthorize") {
                location.href = "/UnAuthrize";
            }
        }

    })
      .error(function OnError(xhr, textStatus, err) {

          if (failureCallBack != null) {
              var obj = jQuery.parseJSON(xhr.responseText);
              var errObj = new Object();
              errObj.Message = obj.Message;

              if (obj.ModelState != null)
                  errObj.ModelState = obj.ModelState;

              errObj.status = xhr.status;
              errObj.statusText = xhr.statusText;
              failureCallBack(errObj)
          }
      });
}

angus.isoft.core.doPostOperationWithOutLoading = function (url, object, successCallBack, failureCallBack) {
    //
    $.ajax({
        url: this.Config.APIBaseUrl + url,
        cache: false,
        type: 'POST',
        contentType: 'application/json',
        dataType: "json",
        data: JSON.stringify(object),
        statusCode: {
            200: function (data) {
                successCallBack(data)
            }
        },
        beforeSend: function () {

        },
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
            var unauthorize = XMLHttpRequest.getResponseHeader("authorize");
            if (unauthorize == "unauthorize") {
                location.href = "/UnAuthrize";
            }
        }

    })
      .error(function OnError(xhr, textStatus, err) {

          if (failureCallBack != null) {
              var obj = jQuery.parseJSON(xhr.responseText);
              var errObj = new Object();
              errObj.Message = obj.Message;

              if (obj.ModelState != null)
                  errObj.ModelState = obj.ModelState;

              errObj.status = xhr.status;
              errObj.statusText = xhr.statusText;
              failureCallBack(errObj)
          }
      });


}
//sync
angus.isoft.core.doPostOperationAsyncFalse = function (url, object, successCallBack, failureCallBack) {
    //

    $.ajax({
        url: this.Config.APIBaseUrl + url,
        cache: false,
        async: false,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(object),
        statusCode: {
            200 /*Created*/: function (data) {
                successCallBack(data)
            }
        },
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
        }
    })
      .error(function OnError(xhr, textStatus, err) {

          if (failureCallBack != null) {
              var obj = jQuery.parseJSON(xhr.responseText);
              var errObj = new Object();
              errObj.Message = obj.Message;

              if (obj.ModelState != null)
                  errObj.ModelState = obj.ModelState;

              errObj.status = xhr.status;
              errObj.statusText = xhr.statusText;
              failureCallBack(errObj)
          }
      });


}
// Web api - Http delete operation - delete a record
angus.isoft.core.doDeleteOperation = function (url, object, successCallBack, failureCallBack) {
    $.ajax({
        url: this.Config.APIBaseUrl + url,
        cache: false,
        type: 'DELETE',
        data: JSON.stringify(object),
        contentType: 'application/json; charset=utf-8',
        complete: function (XMLHttpRequest, textStatus) {
            var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus");
            if (sessionstatus == "timeout") {
                location.href = "/";
            }
        }
    })
    .success(function (data) { successCallBack(data); })
    .fail(
        function (xhr, textStatus, err) {
            if (failureCallBack != null)
                failureCallBack(xhr, textStatus, err);
        });
}

// Validation message
angus.isoft.core.showErrors = function (submitForm, err) {
    var validator = $("#" + submitForm).validate();
    var iCnt = 0;
    errors = [];
    $.each(err.ModelState, function (key, value) {
        var pieces = key.split('.');
        key = pieces[pieces.length - 1];
        errors[key] = value[0];

    });

    validator.showErrors(errors);
}
angus.isoft.core.ShowMessage = function (Message, Type) {
    if (Type == '' || Type == undefined) {
        Type = 'success';
    }
    if (Message == '' || Message == undefined) {
        Message = '没有信息显示';
    }
    //不在里面，直接显示成功
    if (Type != "success" && Type != "warning" && Type != "error" && Type != "info")
    {
        Type = 'warning';
    }
    var shortCutFunction = Type.toString();
    var msg = Message;
    var title = '';
    var $showDuration = 1000;
    var $hideDuration = 1000;
    var $timeOut = 1000;
    var $extendedTimeOut = 1000;
    var $showEasing = 'swing';
    var $hideEasing = 'linear';
    var $showMethod = 'fadeIn';
    var $hideMethod = 'fadeOut';


    toastr.options = {
        closeButton: true,
        debug: false,
        positionClass: 'toast-top-center',
        onclick: null
    };

    if ($('#addBehaviorOnToastClick').prop('checked')) {
        toastr.options.onclick = function () {
            alert('You can perform some custom action after a toast goes away');
        };
    }

    if ($showDuration.length) {
        toastr.options.showDuration = $showDuration;
    }

    if ($hideDuration.length) {
        toastr.options.hideDuration = $hideDuration;
    }

    if ($timeOut.length) {
        toastr.options.timeOut = $timeOut;
    }

    if ($extendedTimeOut.length) {
        toastr.options.extendedTimeOut = $extendedTimeOut;
    }

    if ($showEasing.length) {
        toastr.options.showEasing = $showEasing;
    }

    if ($hideEasing.length) {
        toastr.options.hideEasing = $hideEasing;
    }

    if ($showMethod.length) {
        toastr.options.showMethod = $showMethod;
    }

    if ($hideMethod.length) {
        toastr.options.hideMethod = $hideMethod;
    }
    var $toast = toastr[shortCutFunction](msg, title); // Wire up an event handler to a button in the toast, if it exists
    $toastlast = $toast;


}
//angus.isoft.core.ShowMessage = function (msg, iserror, removeOtherMessages) {
//    if (msg == angus.isoft.core.Config.ajaxProcessedText && $('div._messagediv:not(:contains(' + angus.isoft.core.Config.ajaxProcessingText + '))').length > 0) {
//        return;
//    }
//    var messageBoxid = "messagediv" + Math.round(Math.random() * 1000);
//    if ($("#" + messageBoxid).length == 0) {
//        if (removeOtherMessages != false) {
//            $("._messagediv").remove();
//        }
//        var message = $("<div id='" + messageBoxid + "' style='height:1px;' class='_messagediv messagediv displaynone'><div class='statusMessage rb-a-4 _status'><span class='_message'></span></div></div>");
//        $(document.body).append(message);
//    }
//    if (iserror != undefined && iserror == false) {
//        $("#" + messageBoxid + " ._status").removeClass("error").addClass("message");
//    }
//    else {
//        $("#" + messageBoxid + " ._status").removeClass("message").addClass("error");
//    }
//    var width = $(window).width() - $("#" + messageBoxid + "").width();

//    $("#" + messageBoxid + " ._message").html(msg);
//    $("#" + messageBoxid).slideDown("slow", function () { $(this).css({ "height": "50px" }) });

//    setTimeout('$("._messagediv").slideUp("normal",function(){$(this).remove()})', 3000);
//}

angus.isoft.core.Validator = function (obj, url, rules, message, successCallback) {
    $('#' + obj).validate({
        submitHandler: function (form) {
            $(form).ajaxSubmit({
                url: url,
                dataType: "json",
                type: "post", //('#' + obj).serialize(),
                success: function (response) {
                    successCallback(response);
                }
            });
        },
        focusInvalid: true,
        onfocusout: function (element) { $(element).valid(); },
        rules: rules,
        messages: message,
        errorElement: "div",
        errorClass: "error_info",
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.form-control').addClass('highlight_red');
        },
        success: function (element) {
            $(element).siblings('.form-control').removeClass('highlight_red');
            $(element).siblings('.form-control').addClass('highlight_green');
            $(element).remove();

        }
    });
}

var GlobalLoader = {
    ShowLoader: function () {
        if ($('.blockUI').length == 0) {
            var common = EsdPec.Cloud.NewGeneration.lang.Common;
            $.blockUI({ message: '<img src="/Content/images/loading-spinner-grey.gif" /> ' + common.Wait + '...' });
        }
    },
    HideLoader: function () {
        $.unblockUI();
    }
}
if ($.blockUI) {
    var common = EsdPec.Cloud.NewGeneration.lang.Common;
    $(document).ajaxStart(
        $.blockUI({ message: '<img src="/Content/images/loading-spinner-grey.gif" /> ' + common.Wait + '...' })

        ).ajaxStop(
        $.unblockUI()
    );
}