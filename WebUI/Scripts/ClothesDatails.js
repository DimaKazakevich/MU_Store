$(function () {
    $('body').on('click', 'button#addtobasket', function (e) {
        var selectedSize = $("input[name='size']:checked").val();
        if (selectedSize == undefined) {
            var text = $(this).html("Select Your Size");
            $("button#addtobasket").css('color', "#b61c1c");
            setTimeout(function () { $(text).html("Add To Basket"), $("button#addtobasket").css('color', "white"); }, 2000);
        }
        else {
            var url = "/Basket/AddToBasketWithSize";
            var Article = $(this).data("id");
            $.get(url, { clothesId: Article, size: selectedSize }, function (data) {
                $(".basket").html(data);
            }).done(function () {
                $("button#addtobasket").html("Go To Basket");
                $("button#addtobasket").css('background-color', "#b61c1c");
                $("button#addtobasket").attr("id", "gotobasket");
            })
        }
    })
});

$(function () {
    $("li.productSize").one("click", (function (e) {
        $("button#addtobasket").css('background-color', "#00a92c");
        $("button#addtobasket").css('color', "white");
        $("button#addtobasket").html("Add To Basket");
    }));
});

$(window).load(function (event) {
    let flag = false;
    $("input[name='size']").each(function () {
        if ($(this).is(":checked")) {
            flag = true;
        }
    })
    if (flag) {
        $("button#addtobasket").css('background-color', "#00a92c");
    }
    else {
        $("button#addtobasket").css('background-color', "#999");
    }
});

$(function () {
    //$("button#addToBasketWithoutSize").click(function (e) {
    $('body').on('click', 'button#addToBasketWithoutSize', function (e) {
        e.preventDefault();

        var url = "/Basket/AddToBasketWithoutSize";
        let Article = $(this).data("id");

        $.get(url, { clothesId: Article }, function (data) {
            $(".basket").html(data);
        }).done(function () {
            $("button#addToBasketWithoutSize").html("Go To Basket");
            $("button#addToBasketWithoutSize").css('background-color', "#b61c1c");
            $("button#addToBasketWithoutSize").attr("id", "gotobasket");
        })
    });
});


$(function () {
    $("li.productSize").click((function (e) {
        if ($("button#gotobasket").text() == "Go To Basket") {
            $("button#gotobasket").html("Add To Basket");
            $("button#gotobasket").css('background-color', "#00a92c");
            $("button#gotobasket").attr("id", "addtobasket");
        }
    }));
});