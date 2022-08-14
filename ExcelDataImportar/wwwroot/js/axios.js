function postDataWithFile(url, data, dataType, callback){
    $.ajax({
        url,
        method: 'post',
        data: data,
        dataType: dataType,
        contentType: false,
        processData: false,
        success: function(data) {
            callback(data);
        },
        error: function (errors) {
            callback(errors);
        }
    });
}

function postData(url, data, dataType, callback) {
    $.ajax({
        url,
        method: 'post',
        data: data,
        dataType: dataType,
        success: function(data) {
            callback(data);
        },
        error: function (errors) {
            callback(errors);
        }
    });
}

function getData(url, method, data, dataType, callback) {
    $.ajax({
        url,
        method,
        data: data,
        dataType: dataType,
        success: function (data) {
            callback(data);
        },
        error: function (errors) {
            callback(errors);
        }
    });
}

function getFormData(formName){
    var formData = {};
    $(formName).find('.form-control').each(function (index, control) {
        if (this.tagName.toLowerCase() == 'select' || this.tagName.toLowerCase() == 'input') {
            formData[this.name] = $(this).val()
        }
    });

    return formData;
}

function is401(data){
    let is401 = false;
    if (data.status != undefined) {
        if (data.status == 401) {
            is401 = true;
        }
    } else if (data.status) {
        console.log(data);
    }
    return is401;
}

window.Axios = {
    postData, postDataWithFile, getData, getFormData, is401
};