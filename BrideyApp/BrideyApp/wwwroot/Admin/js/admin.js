﻿$(function () {

        $(document).on("click", ".delete-slider", function (e) {
            e.preventDefault();
            let Id = $(this).parent().parent().attr("data-id");
            let deletedElem = $(this).parent().parent();
            let data = { id: Id }
            let tbody = $(this).parent().parent().parent();


            $.ajax({
                url: "slider/delete",
                type: "Post",
                data: data,
                success: function (res) {
                    $(deletedElem).remove();
                    if ($(tbody).length == 0) {
                        $(".table").remove();
                    }
                }

            })
        })

        $(document).on("click", ".delete-homeBanner", function (e) {
            e.preventDefault();
            let Id = $(this).parent().parent().attr("data-id");
            let deletedElem = $(this).parent().parent();
            let data = { id: Id }
            let tbody = $(this).parent().parent().parent();


            $.ajax({
                url: "homeBanner/delete",
                type: "Post",
                data: data,
                success: function (res) {
                    $(deletedElem).remove();
                    if ($(tbody).length == 0) {
                        $(".table").remove();
                    }
                }

            })
        })

   
        $(document).on("click", ".delete-aboutUs", function (e) {
            e.preventDefault();
            let Id = $(this).parent().parent().attr("data-id");
            let deletedElem = $(this).parent().parent();
            let data = { id: Id }
            let tbody = $(this).parent().parent().parent();

            $.ajax({
                url: "aboutUs/delete",
                type: "Post",
                data: data,
                success: function (res) {
                    $(deletedElem).remove();
                    if ($(tbody).length == 0) {
                        $(".table").remove();
                    }
                }

            })
        })
    $(document).on("click", ".delete-bride", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "bride/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })

})

