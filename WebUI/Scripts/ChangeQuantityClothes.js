$(function () {

    $("a.incClothes").click(function (e) {
        e.preventDefault();

        let Article = $(this).data("id");
        let url = "/Basket/IncrementClothes";

        $.getJSON(url, { clothesId: Article }, function (data) {
            $("td.quantity[data-id=" + Article + "]").html(data.quantity);
 
            let price = data.quantity * data.price;
            let priceHtml = price + " $";

            $(".cost[data-id=" + Article + "]").html(priceHtml);

            let currentTotalCostSide = parseFloat($("span.price").text())
            let newTotalCostSide = currentTotalCostSide + data.price + " $";
            $("span.price").text(newTotalCostSide);

            let currentTotalCostHeader = parseFloat($("li.price").text())
            let newTotalCostHeader = currentTotalCostHeader + data.price + " $";
            $("li.price").text(newTotalCostHeader);
    
            let currentTotalItems = parseFloat($("li.summaryItems").text())
            let newTotalItems = currentTotalItems + 1;
            $("li.summaryItems").text(newTotalItems);
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
                $("td.quantity[data-id=" + Article + "]").html(data.quantity);

                let price = data.quantity * data.price;
                let priceHtml = price + " $";

                $(".cost[data-id=" + Article + "]").html(priceHtml);

                let currentTotalCostHeader = parseFloat($("span.price").text())
                let newTotalCost = currentTotalCostHeader - data.price + " $";
                $("span.price").text(newTotalCost);

                let currentTotalCostSide = parseFloat($("li.price").text())
                let newTotalCostSide = currentTotalCostSide - data.price + " $";
                $("li.price").text(newTotalCostSide);
        
                let currentTotalItems = parseFloat($("li.summaryItems").text())
                let newTotalItems = currentTotalItems - 1;
                $("li.summaryItems").text(newTotalItems);
            }
        });
    });
});

$(function () {
    $("a.removeClothesWithSize").click(function (e) {
        e.preventDefault();

        let article = $(this).data("id");
        let size = $(this).data("content");
        let url = "/Basket/RemoveFromBasketWithSize";

        $.get(url, { clothesId: article, size: size }, function () {
            location.reload();
        });
    });
});

$(function () {
    $("a.removeClothesWithoutSize").click(function (e) {
        e.preventDefault();

        let article = $(this).data("id");
        let url = "/Basket/RemoveFromBasket";

        $.get(url, { clothesId: article }, function () {
            location.reload();
        });
    });
});

$(function () {

    $("a.incClothesWithSize").click(function (e) {
        e.preventDefault();

        let Article = $(this).data("id");
        let size = $(this).data("content");
        let url = "/Basket/IncrementClothesWithSize";

        $.getJSON(url, { clothesId: Article, size }, function (data) {
            $("td.quantity[data-id=" + Article + "][data-content=" + size + "]").html(data.quantity);

            let price = data.quantity * data.price;
            let priceHtml = price + " $";

            $(".cost[data-id=" + Article + "][data-content=" + size + "]").html(priceHtml);

            let currentTotalCostSide = parseFloat($("span.price").text())
            let newTotalCostSide = currentTotalCostSide + data.price + " $";
            $("span.price").text(newTotalCostSide);

            let currentTotalCostHeader = parseFloat($("li.price").text())
            let newTotalCostHeader = currentTotalCostHeader + data.price + " $";
            $("li.price").text(newTotalCostHeader);

            let currentTotalItems = parseFloat($("li.summaryItems").text())
            let newTotalItems = currentTotalItems + 1;
            $("li.summaryItems").text(newTotalItems);
        });
    });
});

$(function () {
    $("a.decClothesWithSize").click(function (e) {
        e.preventDefault();

        let $this = $(this);
        let Article = $(this).data("id");
        let size = $(this).data("content");
        let url = "/Basket/DecrementClothesWithSize";

        $.getJSON(url, { clothesId: Article, size }, function (data) {

            if (data.quantity == 0) {
                $this.parent().fadeOut("fast", function () {
                    location.reload();
                });
            }
            else {
                $("td.quantity[data-id=" + Article + "][data-content=" + size + "]").html(data.quantity);

                let price = data.quantity * data.price;
                let priceHtml = price + " $";

                $(".cost[data-id=" + Article + "][data-content=" + size + "]").html(priceHtml);

                let currentTotalCostHeader = parseFloat($("span.price").text())
                let newTotalCost = currentTotalCostHeader - data.price + " $";
                $("span.price").text(newTotalCost);

                let currentTotalCostSide = parseFloat($("li.price").text())
                let newTotalCostSide = currentTotalCostSide - data.price + " $";
                $("li.price").text(newTotalCostSide);

                let currentTotalItems = parseFloat($("li.summaryItems").text())
                let newTotalItems = currentTotalItems - 1;
                $("li.summaryItems").text(newTotalItems);
            }
        });
    });
});

$(function () {
    $("img.addtobasket").click(function (e) {
        e.preventDefault();

        var modal = document.getElementById("sizeModal");
        modal.style.display = "block";

        $.get($(this).data("targeturl"), function (data) {
            $(".modal-body").html(data);
        });
    })
});

$(function () {
    $("img.addToBasketWithoutSize").click(function (e) {
        e.preventDefault();

        var url = "/Basket/AddToBasketWithoutSize/";
        let Article = $(this).data("id");

        $.get(url, { clothesId: Article }, function (data) {
            $(".basket").html(data);
        }).done(function () {

            setTimeout(function () {
                $("div#itemAddedMessage-" + Article).fadeIn("fast");
            }, 300);

            setTimeout(function () {
                $("div#itemAddedMessage-" + Article).fadeOut("fast");
            }, 3000);
        })
    });
});





