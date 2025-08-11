// /Script
(function ($) {
    "use strict";

    $(function () {
        var ddl = $("#ddlCompras");
        var saldo = $("#txtSaldo");
        var desc = $("#txtDescripcion");
        var monto = $("#txtMonto");
        var alerta = $("#alerta");
        var alertaMsg = $("#alertaMsg");

        function showError(msg) {
            if (!alerta.length) return;
            alertaMsg.text(msg);
            alerta.removeClass("d-none").addClass("show");
            window.scrollTo({ top: 0, behavior: "smooth" });
            setTimeout(function () { alerta.addClass("d-none").removeClass("show"); }, 4500);
        }

        function getSaldoUrl() {
            return (window.APP && APP.urls && APP.urls.getSaldo) ? APP.urls.getSaldo : "/Abonos/GetSaldo";
        }

        ddl.on("change", function () {
            var id = $(this).val();
            if (!id) { saldo.val(""); desc.val(""); return; }

            $.getJSON(getSaldoUrl(), { idCompra: id })
                .done(function (r) {
                   
                    var s = parseFloat(r.saldo);
                    if (isNaN(s)) s = 0;
                    saldo.val(s.toFixed(5));
                    desc.val(r.descripcion || "");
                })
                .fail(function () { showError("No se pudo cargar el saldo de la compra seleccionada."); });
        });

        if (ddl.val()) {
            ddl.trigger("change");
        }

        $("#frmAbono").on("submit", function (e) {
            var s = parseFloat((saldo.val() || "0").replace(",", "."));
            var m = parseFloat((monto.val() || "0").replace(",", "."));
            if (!ddl.val()) { e.preventDefault(); showError("Seleccione una compra válida."); return; }
            if (isNaN(s) || s <= 0) { e.preventDefault(); showError("La compra seleccionada no posee saldo."); return; }
            if (isNaN(m) || m <= 0) { e.preventDefault(); showError("Ingrese un monto mayor que 0."); return; }
            if (m > s + 1e-9) { e.preventDefault(); showError("El abono no puede ser mayor que el saldo anterior."); return; }
        });
    });
})(jQuery);
