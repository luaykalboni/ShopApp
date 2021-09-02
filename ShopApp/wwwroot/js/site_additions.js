
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



        });
    });
}


function remove(url1) {
    console.log("Remove");
    console.log(url1);
    const btns = document.querySelectorAll(`[data-action=${CSS.escape(url1.data_action)}]`);
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
                url: url1.post_remove_fromCart,
                data: { proId: pro_id, userName: userName }
                //    dataType: "json"
            });



        });
    });
}

