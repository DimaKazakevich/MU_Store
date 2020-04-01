$(function () {

    $("a.incClothes").click(function (e) {
        e.preventDefault();

        let Article = $(this).data("id");
        let url = "/Basket/IncrementClothes";

        $.getJSON(url, { clothesId: Article }, function (data) {
            $("td#quantity" + Article).html(data.quantity);
 
            let price = data.quantity * data.price;
            let priceHtml = price + " $";

            $("td#cost" + Article).html(priceHtml);

            $("li.summary").html(data.quantity + " items(Product code: " + Article + ") / " + priceHtml);

            let gt = parseFloat($("td#totalCost").text())
            let grandtotal = gt + data.price + " $";

            $("td#totalCost").text(grandtotal);
        });
    });
});

$(function () {

    $("a.decClothes").click(function (e) {
        e.preventDefault();

        let $this = $(this);
        let Article = $(this).data("id");
        let url = "/Basket/DecrementClothes";

        $.getJSON(url, { clothesId: Article }, function (data) {

            if (data.quantity == 0) {
                $this.parent().fadeOut("fast", function () {
                    location.reload();
                });
            }
            else {
                $("td#quantity" + Article).html(data.quantity);

                let price = data.quantity * data.price;
                let priceHtml = price + " $";

                $("td#cost" + Article).html(priceHtml);

                $("li.summary").html(data.quantity + " items(Product code: " + Article + ") / " + priceHtml);

                let gt = parseFloat($("td#totalCost").text());
                let grandtotal = (gt - data.price) + " $";

                $("td#totalCost").text(grandtotal);
            }
        });
    });
});


$(function () {

    $("a.removeClothes").click(function (e) {
        e.preventDefault();

        let $this = $(this);
        let Article = $(this).data("id");
        let url = "/Basket/RemoveFromBasket";

        $.get(url, { clothesId: Article }, function () {
            location.reload();
        });
    });
});