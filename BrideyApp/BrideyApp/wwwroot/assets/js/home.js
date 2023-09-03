//slider

var swiper = new Swiper(".mySwiper", {
  spaceBetween: 30,
  centeredSlides: true,
   autoplay: {
       delay: 2500,
       disableOnInteraction: false,
   },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});


//blog-cards-slider
$('.blog-slider').slick({
  dots: true,
  infinite: false,
  speed: 300,
  slidesToShow: 3,
    slidesToScroll: 4,
    autoplay: true,
    auttoplaySpeed: 1000,
  responsive: [
    {
      breakpoint: 1024,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3,
        infinite: true,
        dots: true
      }
    },
    {
      breakpoint: 920,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 1,
        slidesToScroll: 1
      }
    },

  ]
});

