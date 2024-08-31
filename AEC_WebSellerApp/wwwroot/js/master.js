//var Loader = {
//    loaderStart: function (data) {
//        //$("#loadSpin").show();
//        $.blockUI();
//    },
//    loaderFinish: function () {
//        //$("#loadSpin").hide();
//        $(".tooltipShow").tooltip();
//        $.unblockUI();
//    }
//}

//$(window).on('beforeunload', function () {
//    Loader.loaderStart();
//    Loader.loaderFinish();
//});

//$(document).ajaxStart(function (e) {
//    Loader.loaderStart();
//    //$(this).html("<img src='https://media.tenor.com/images/d6cd5151c04765d1992edfde14483068/tenor.gif'> Bekleyiniz...");
//});

//$(document).ajaxStop(function (e) {
//    Loader.loaderFinish();
//});

//$(document).ajaxError(function () {
//    Loader.loaderFinish();
//    toastr["error"]("Sistemde bir hata oluştu! Daha sonra tekrar deneyiniz.");
//});




function FormatTurkishLiras(price) {

    var currency_symbol = "₺"

    var formattedOutput = new Intl.NumberFormat('tr-TR', {
        style: 'currency',
        currency: 'TRY',
        minimumFractionDigits: 2,
    });

    return formattedOutput.format(price);
}

function VirgulAyarla(sayi, virguldenSonraKacBasamak) {
    return sayi.toFixed(virguldenSonraKacBasamak);
}

var Product = (function () {

    function Product(ImagePath, ProductName, Price, Amount) {
        this.ImagePath = ImagePath;
        this.ProductName = ProductName;
        this.Amount = Amount;
        this.Price = Price;
    }

    Product.prototype.GetProductPrice = function () {
        return this.Price;
    }

    return Product;

})();


//"error"
//"success"
//"warning"
function MessageBox(type, msg) {

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "preventDuplicates": true,
        "positionClass": "toast-top-right", //Ekran görüntü seçenekleri: top-left(Yukarı Sol), top(yukari orta), bottom-left(aşağı sol), bottom(aşağı orta), bottom-right(aşağı sağ)
        "showDuration": "200",
        "hideDuration": "2000",
        "timeOut": "2000",
        "extendedTimeOut": "2000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    toastr[type](msg);
}

function MakeDataTable(tableId) {
    $('#' + tableId).DataTable({
        language: {
            url: '/lib/jquery-datatables/tr-lang/tr.json'
        }
    });
}