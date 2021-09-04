
function init(url) {

    //Select each Remove btn in UserCart and addEventListener
    const btns = document.querySelectorAll(`[data-action=${CSS.escape(url.data_action)}]`);

    btns.forEach(btn => {
        btn.addEventListener("click", e => {
            var key = e.target;
            var idJQ = "#" + key.id;
            var pro_id = $(idJQ).data('productid');
            var userName = $(idJQ).data('userid');

            $.ajax({
                type: "POST",
                url: url.post_remove_fromCart,
                data: { proId: pro_id, userName: userName }
            });

            RemoveFromTable(pro_id);
            decreseCartQty();
        });
    });
}

function RemoveFromTable(proId) {

    const rows = document.querySelectorAll(`[data-action='row']`);
    rows.forEach(r => {
        var id = "#" + r.id;
        var itemId = $(id).data('proid');
        var itemQty = parseInt($(id).data('qty'));


        if (itemId == proId && itemQty == 1) {
            document.getElementById(r.id).style.display = 'none';
            const totalPrice = document.querySelectorAll(`[data-action='totalPrice']`);
            totalPrice.forEach(t => {
                var id = "#" + t.id;
                var itemId = $(id).data('proid');
                if (itemId == proId) {
                    $(id).data('proactive', false);
                }
            });
            UpdateTotalPrice();


        }
        else {
            if (itemId == proId && itemQty > 1) {
                const qtysLable = document.querySelectorAll(`[data-action='qty']`);
                qtysLable.forEach(l => {
                    var id_l = "#" + l.id;
                    var newQty = itemQty - 1;
                    if ($(id_l).data('proid') == proId) {
                        l.innerHTML = " x" + newQty;
                        $(id).data('qty', newQty);

                        UpdateTotalPrice_Line(newQty, proId);



                    }
                });

            }

        }

    });


}

function UpdateTotalPrice_Line(newQty, proId) {

    const totalPrice = document.querySelectorAll(`[data-action='totalPrice']`);
    totalPrice.forEach(t => {
        var id = "#" + t.id;
        var price = $(id).data('price');
        var itemId = $(id).data('proid');

        if (itemId == proId) {

            t.innerHTML = price * newQty;
        }

    });
    UpdateTotalPrice();


}

function UpdateTotalPrice() {
    var sum = 0;
    const totalPrice = document.querySelectorAll(`[data-action='totalPrice']`);

    totalPrice.forEach(t => {
        var id = "#" + t.id;
        var totalLine = parseFloat(t.innerHTML);

        if ($(id).data('proactive') == true) {
            sum += totalLine;
        }

    });

    document.getElementById('totalPricestrong').innerHTML = sum;
    $("#total_price_payment").innerHTML = sum;


}

function decreseCartQty() {
    const qty_lbl = document.querySelector(`[data-action='cartQty']`);
    var idJQ = "#" + qty_lbl.id;
    var qty = parseInt($(idJQ).data('qty')) - 1;
    qty_lbl.innerHTML = qty;
    $(idJQ).data('qty', qty);
}


