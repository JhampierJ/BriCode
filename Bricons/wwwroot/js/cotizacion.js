let prod = JSON.parse(localStorage.getItem("ids"));
let totalP = document.getElementById("totalP");
let total = 0.0;
productoss.forEach((pt) => {
    valorLocalStorague.forEach((pt2) => {
        if (pt.id == pt2.id) {
            pt.value = pt2.cantidad;

            let precio = document.getElementById("p_" + pt.id);
            let valor = precio.getAttribute('data-valor');
            let precioNumerico = parseFloat(valor) * parseFloat(pt.value);
            precio.textContent = "S/" + (precioNumerico.toFixed(2));
            total += precioNumerico;
        }
    });
});
totalP.textContent = total.toFixed(2);
//updateTotal();
