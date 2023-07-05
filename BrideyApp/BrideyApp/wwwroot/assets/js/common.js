$(document).ready(function () {

    //Navbar fixed
    $(window).scroll(function () {
        var navBar = $('#nav-down')
        var decorImg = $('.decor-img')
        var userIcons = $('#nav-down .right-area')
        var topMenu = $('#nav-down .top-menu')
        var backTop = $('.back-top')

        scroll = $(window).scrollTop();
        if (scroll >= 150) {
            navBar.addClass('nav-down')

            navBar.css({
                'position': 'fixed',
                'top': '0',
                'left': '0',
                'right': '0',
                'z-index': '99999',
                'height': '87px',

                'background-color': 'white',
                'box-shadow': 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
            });
            topMenu.css({
                'gap': '2%',
            })
            decorImg.removeClass('d-none')
            userIcons.removeClass('d-none')
            backTop.removeClass('d-none')

        } else {
            navBar.css({
                'position': 'relative',
                'box-shadow': 'none',
                'z-index': '0',

            });
            decorImg.addClass('d-none')
            userIcons.addClass('d-none')
            backTop.addClass('d-none')

        }
    });



     //HAMBURGER
    $(".hamburger").on("click", function () {
        $("#overlay").removeClass("d-none");
        $(".side-bar").css("transform", `translateX(${0}px)`);
        $("body").css("overflow-y", 'hidden');
    })


    $(".close").on("click", function () {
        $("#overlay").addClass("d-none");
        $(".side-bar").css("transform", `translateX(${-239}px)`);
        $("body").css("overflow", 'scroll');

    })
    $("#overlay").on("click", function () {
        $("#overlay").addClass("d-none");
        $(".side-bar").css("transform", `translateX(${-239}px)`);
        $("body").css("overflow", 'scroll');
    })



    //add cart
    AddToCart(".add-to-cart-icon", "/Shop/AddToCart");

    function AddToCart(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            let id = $(this).attr("data-id");
            let data = { id: id };
            let count = (".count-basket");
            $.ajax({
                type: "Post",
                url: url,
                data: data,
                success: function (res) {
                    $(count).text(res);
                }
            })
            return false;
        })
    }
    //delete product from basket
    $(document).on("click", ".delete-product", function () {

        let id = $(this).parent().parent().attr("data-id");
        let prod = $(this).parent().parent();
        let tbody = $(".tbody-basket").children();
        let data = { id: id };

        $.ajax({
            type: "Post",
            url: `Cart/DeleteDataFromBasket`,
            data: data,
            success: function (res) {
                if ($(tbody).length == 1) {
                    $(".product-table").addClass("d-none");
                }
                $(prod).remove();
                $(".count-basket").text(res)
                grandTotal();
            }
        })
        return false;
    })

    //change product count
    $(document).on("click", ".inc", function () {
        let id = $(this).parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().prev().children().eq(1).text());
        let total = $(this).parent().next().children().eq(1);
        let count = $(this).prev();

        $.ajax({
            type: "Post",
            url: `Cart/IncrementProductCount?id=${id}`,
            success: function (res) {
                res++;
                subTotal(res, nativePrice, total, count)
                 $(".count-basket").text(res)
                grandTotal();
            }
        })
    })

    $(document).on("click", ".dec", function () {
        let id = $(this).parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().prev().children().eq(1).text());
        let total = $(this).parent().next().children().eq(1);
        let count = $(this).next();

        $.ajax({
            type: "Post",
            url: `Cart/DecrementProductCount?id=${id}`,
            success: function (res) {
                if ($(count).val() == 1) {
                    return;
                }
                res--;
                subTotal(res, nativePrice, total, count)
                $(".count-basket").text(res)
                grandTotal();
            }
        })
    })

    function grandTotal() {
        let tbody = $(".tbody-basket").children()
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(4).children().eq(1).text())
            sum += price
        }
        $(".grand-total").text(sum);
    }

    function subTotal(res, nativePrice, total, count) {
        $(count).val(res);
        let subtotal = parseFloat(nativePrice * $(count).val());
        $(total).text(subtotal.toString(0.00));
    }

    $(document).on("submit", ".searchbox-area", function () {
        let value = $(".input-search").val();
        let url = `/Shop/Search?searchText=${value}`;
        window.location.assign(url);
        return false;
    })


});


//Headere qaytaran icon
$('.back-top').click(function () {
    $('html').animate({
        scrollTop: 0
    }, 1)

})

let userIconn = document.querySelector(".user-icon")
userIconn.addEventListener("click", function () {
    document.querySelector(".login-register").classList.toggle("d-none")
})
//area kenarina toxunanda hemin hissenin silinmesi

document.addEventListener("click", function (e) {
    if (!!!e.target.closest(".user-icon")) {
        if (!$(".login-register").hasClass("d-none")) {
            $(".login-register").addClass("d-none")
        }
    }
})


//see login -register in click user-icon
let userIcon = document.querySelector(".user-icon-responsive");
let loginRegister = document.querySelector(".login-register-responsive");
userIcon.addEventListener("click", function () {
    loginRegister.classList.remove("d-none")
    document.getElementById("overlay").style.display = "block"
})


document.addEventListener("click", function (e) {
    if (!!!e.target.closest(".user-icon-responsive")) {
        if (!$(".login-register-responsive").hasClass("d-none")) {
            $(".login-register-responsive").addClass("d-none")
        }
    }
})











