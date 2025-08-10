// /Script/abonos-create.js
(function ($) {
    "use strict";

    $(function () {
        const ddl = $("#ddlCompras");
        const saldo = $("#txtSaldo");
        const desc = $("#txtDescripcion");
        const monto = $("#txtMonto");
        const alerta = $("#alerta");
        const alertaMsg = $("#alertaMsg");

        ddl.on("change", function () {
            const id = $(this).val();
            if (!id) { saldo.val(""); desc.val(""); return; }

            const url = (window.APP && APP.urls && APP.urls.getSaldo) || "/Abonos/GetSaldo";

            $.getJSON(url, { idCompra: id })
                .done(function (r) {
                    saldo.val(parseFloat(r.saldo).toFixed(5));
                    desc.val(r.descripcion);
                })
                .fail(function () {
                    showError("No se pudo cargar el saldo de la compra seleccionada.");
                });
        });

        $("#frmAbono").on("submit", function (e) {
            const s = parseFloat((saldo.val() || "0").replace(",", "."));
            const m = parseFloat((monto.val() || "0").replace(",", "."));

            if (isNaN(s) || s <= 0) { e.preventDefault(); showError("Seleccione una compra válida para cargar su saldo."); return; }
            if (isNaN(m) || m <= 0) { e.preventDefault(); showError("Ingrese un monto de abono mayor que 0."); return; }
            if (m > s + 1e-9) { e.preventDefault(); showError("El abono no puede ser mayor que el saldo anterior."); return; }
        });

        function showError(msg) {
            alertaMsg.text(msg);
            alerta.removeClass("d-none").addClass("show");
            window.scrollTo({ top: 0, behavior: "smooth" });
            setTimeout(() => alerta.addClass("d-none").removeClass("show"), 4500);
        }
    });
})(jQuery);
