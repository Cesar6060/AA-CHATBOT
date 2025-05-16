window.blazorExtensions = {
    SaveToCookies: function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    },
    ShowModal: function (modalId) {
        var modal = new bootstrap.Modal(document.getElementById(modalId));
        modal.show();
    },
    GetFromCookies: function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    },
    DeleteFromCookies: function (name) {
        document.cookie = name + "=; Max-Age=-99999999; path=/";
    },
    SetLocalStorage: function(key, value) {
        if(localStorage.getItem(key)) localStorage.removeItem(key);
        localStorage.setItem(key, value);
    },
    GetLocalStorage: function(key) {
        if(localStorage.getItem(key)){
            return localStorage.getItem(key);
        }
        return "";
    }
};
function openPdf(pdfPath) {
    window.open(pdfPath, '_blank');
}


