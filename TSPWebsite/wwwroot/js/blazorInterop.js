var blazorInterop = blazorInterop || {};

$(document).ready(function () {
    /* ======= Fixed header when scrolled ======= */
    $(window).bind('scroll', function () {
        var viewportWidth = $(window).width();

        if ($(window).scrollTop() > 0) { //leave top
            $('#topbox').addClass('d-none');
            $('#topMenuLinks').css('display', '');// default is display none.
        }
        else { //back to top
            if (viewportWidth < 1234) {
                $('#topbox').addClass('d-none');
                $('#topMenuLinks').css('display', '');

            } else {
                $('#topbox').removeClass('d-none');
                $('#topMenuLinks').css('display', 'none');
            }
        }
    });
    $(window).resize(function () {
        var viewportWidth = $(window).width();
        var top = $(window).scrollTop();
        if (viewportWidth < 1234) {
            $('#topbox').addClass('d-none');
            $('#topMenuLinks').css('display', '');
        } else {
            if (top > 0) { //leave top
                $('#topbox').addClass('d-none');
                $('#topMenuLinks').css('display', '');
            } else {
                $('#topbox').removeClass('d-none');
                $('#topMenuLinks').css('display', 'none');
            }
        }
        setParaImageHeight();
    });
    setParaImageHeight();
});
function setParaImageHeight() {
    var paralWidth = $(".mainParallax1").height();
    $(".mainParalImage").css("height", (paralWidth - 40) + "px");
}
function gallery(item) {
    $(".all").removeClass("animate__animated");
    $(".all").removeClass("animate__fadeInUp");
    $(".all").addClass("d-none");
    //
    $("." + item).addClass("animate__animated");
    $("." + item).addClass("animate__fadeInUp");
    $("." + item).removeClass("d-none");
}

blazorInterop.initializeScreenToBodyPage = function () {
    var height = $("#videobackground").height();
    $("body, html").animate({
        scrollTop: (height - 50)
    }, 1000)
};
blazorInterop.initializeTopbox = function () {
    var viewportWidth = $(window).width();
    if (viewportWidth < 1234) {
        $('#topbox').addClass('d-none');
        $('#topMenuLinks').css('display', '');
    } 
};

blazorInterop.initializeHideDropdown = function () {
    $("#navbar-collapse").removeClass("show");
};
blazorInterop.setParaImageHeight = function () {
    setParaImageHeight();
};
// for recaptcha
var My;
(function (My) {
    var reCAPTCHA;
    (function (reCAPTCHA) {
        let scriptLoaded = null;
        function waitScriptLoaded(resolve) {
            if (typeof (grecaptcha) !== 'undefined' && typeof (grecaptcha.render) !== 'undefined')
                resolve();
            else
                setTimeout(() => waitScriptLoaded(resolve), 100);
        }
        function init() {
            const scripts = Array.from(document.getElementsByTagName('script'));
            if (!scripts.some(s => (s.src || '').startsWith('https://www.google.com/recaptcha/api.js'))) {
                const script = document.createElement('script');
                script.src = 'https://www.google.com/recaptcha/api.js?render=explicit';
                script.async = true;
                script.defer = true;
                document.head.appendChild(script);
            }
            if (scriptLoaded === null)
                scriptLoaded = new Promise(waitScriptLoaded);
            return scriptLoaded;
        }
        reCAPTCHA.init = init;
        function render(dotNetObj, selector, siteKey) {
            return grecaptcha.render(selector, {
                'sitekey': siteKey,
                'callback': (response) => { dotNetObj.invokeMethodAsync('CallbackOnSuccess', response); },
                'expired-callback': () => { dotNetObj.invokeMethodAsync('CallbackOnExpired'); }
            });
        }
        reCAPTCHA.render = render;
        function getResponse(widgetId) {
            return grecaptcha.getResponse(widgetId);
        }
        reCAPTCHA.getResponse = getResponse;
    })(reCAPTCHA = My.reCAPTCHA || (My.reCAPTCHA = {}));
})(My || (My = {}));
//# sourceMappingURL=script.js.map