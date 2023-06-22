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



//HAMBURGER


$(document).ready(function () {
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
  });






