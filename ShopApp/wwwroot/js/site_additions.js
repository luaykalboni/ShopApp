
function init(url) {
    //const addToCart_btns = document.querySelectorAll("[data-action='add_to_cart']");

    console.log(url);
    const btns = document.querySelectorAll(`[data-action=${CSS.escape(url.data_action)}]`);
    console.log(btns);

    btns.forEach(btn => {
        btn.addEventListener("click", e => {
            console.log("click");
            var key = e.target;
            var idJQ = "#" + key.id;
            var pro_id = $(idJQ).data('productid');
            var name = $(idJQ).data('name');
            var userName = $(idJQ).data('userid');

            console.log(pro_id);
            console.log(name);



            $.ajax({
                type: "POST",
                url: url.post_add_to_cart,
                data: { proId: pro_id, userName: userName }
                //    dataType: "json"
            });
            increseCartQty();




        });
    });
}




function increseCartQty() {
    const qty_lbl = document.querySelector(`[data-action='cartQty']`);
    var idJQ = "#" + qty_lbl.id;
    var qty = parseInt($(idJQ).data('qty')) + 1;
    qty_lbl.innerHTML = qty;
    $(idJQ).data('qty', qty);
}

//function remove(url1) {
//    console.log("Remove");
//    console.log(url1);
//    const btns = document.querySelectorAll(`[data-action=${CSS.escape(url1.data_action)}]`);
//    console.log(btns);
//    btns.forEach(btn => {
//        btn.addEventListener("click", e => {
//            console.log("click");
//            var key = e.target;
//            var idJQ = "#" + key.id;
//            var pro_id = $(idJQ).data('productid');
//            var name = $(idJQ).data('name');
//            var userName = $(idJQ).data('userid');

//            console.log(pro_id);
//            console.log(name);



//            $.ajax({
//                type: "POST",
//                url: url1.post_remove_fromCart,
//                data: { proId: pro_id, userName: userName }
//                //    dataType: "json"
//            });



//        });
//    });
//}

//function removePresnt(proId) {

//    const rows = document.querySelectorAll(`[data-action='row')}]`);
//    rows.forEach(r => {
//        var id = "#" + r.id;
//        var itemId = $(id).data('proid');
//        var itemQty = $(id).data('qty');


//        if (itemId == proId && itemQty == 1) {
//            console.log(id);
//            console.log(itemId);
//            console.log(itemQty);

//            document.getElementById(r.id).style.display = 'none';
//        }
//        else {
//            if (itemId == proId && itemQty > 1)
//            {

//                console.log(id);
//                console.log(itemId);
//                console.log(itemQty);
//                    const qtysLable = document.querySelectorAll(`[data-action='qty')}]`);
//                    qtysLable.forEach(l => {
//                        var id_l = "#" + l.id;
//                        var newQty = itemQty - 1;
//                        if ($(id_l).data('proid') == proId) {
//                            l.innerHTML = " x" + newQty;
//                            $(id).data('qty') = newQty;

//                        }
//                    });

//                }
//        }
//    });


//}