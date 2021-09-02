

function init(url) {
    const addToCart_btns = document.querySelectorAll("[data-action='add_to_cart']");
    addToCart_btns.forEach(btn => {
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