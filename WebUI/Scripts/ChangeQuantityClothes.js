﻿$(function () {

    $("a#incClothes").click(function (e) {
        e.preventDefault();

        let Article = $(this).data("id");
        let url = "/Basket/IncrementClothes";

        $.getJSON(url, { clothesId: Article }, function (data) {
            $("td#quantity" + Article).html(data.quantity);
 
            let price = data.quantity * data.price;
            let priceHtml = price + " $";

            $("td#cost" + Article).html(priceHtml);

            let currentTotalCost = parseFloat($("td#totalCost").text())
            let newTotalCost = currentTotalCost + data.price + " $";
            $("td#totalCost").text(newTotalCost);

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
    $("a#decClothes").click(function (e) {
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

                let gt = parseFloat($("td#totalCost").text());
                let grandtotal = (gt - data.price) + " $";

                let currentTotalCostHeader = parseFloat($("span.price").text())
                let newTotalCost = currentTotalCostHeader - data.price + " $";
                $("span.price").text(newTotalCost);

                let currentTotalCostSide = parseFloat($("li.price").text())
                let newTotalCostSide = currentTotalCostSide - data.price + " $";
                $("li.price").text(newTotalCostSide);
        
                let currentTotalItems = parseFloat($("li.summaryItems").text())
                let newTotalItems = currentTotalItems - 1;
                $("li.summaryItems").text(newTotalItems);

                $("td#totalCost").text(grandtotal);
            }
        });
    });
});





