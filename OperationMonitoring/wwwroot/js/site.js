
$(document).ready(function () {
    $(".js-btn-edit").click(function () {
        $(this).parent().hide().next().fadeIn();
    });
    $(".js-btn-discard").click(function () {
        $(this).parents(".details-info__item-form, .content-heading__form").hide().prev().fadeIn();
    });
    $(".js-form-show").click(function () {
        $(this).hide().next('.form-pos').fadeIn();
    });

    $("select").each(function(){
        $(this).change(function () {
            $(this).next(".text-danger").hide();
        });
    });

    if( $("#formOpenOrder").length ){
        var $selectContracts = $("select[name='orderContract']");
        var $selectContractsClone = $selectContracts.clone();
        var $selectWells = $("select[name='orderWell']");
        var $selectWellsClone = $selectWells.clone();

        console.log($selectContractsClone);
        
        $("select[name='orderCounterparty']").change(function () {
            $("select[name='orderContract'], select[name='orderWell']").prop("disabled", false);
            $("#formOpenOrder .text-danger").hide();
            var selectedCllient = $(this).children("option:selected").val();

                $selectContracts.find("optgroup").remove();
                var $foundOptgroup = $selectContractsClone.find("optgroup[data-Counterparty='" + selectedCllient + "']");
                if($foundOptgroup.length != 0){
                    $selectContracts.append($foundOptgroup.clone());
                }
                else{
                    $selectContracts.append($selectContractsClone.find("optgroup[data-Counterparty='-1']").clone());
                }

                $selectWells.find("optgroup").remove();
                var $foundOptgroup2 = $selectWellsClone.find("optgroup[data-Counterparty='" + selectedCllient + "']");
                if($foundOptgroup2.length != 0){
                    $selectWells.append($foundOptgroup2.clone());
                }
                else{
                    $selectWells.append($selectWellsClone.find("optgroup[data-Counterparty='-1']").clone());
                }
          
        });
    }

    $(".form-close-maintenance select[name='equipmentStatus']").change(function () {
        if($(this).children("option:selected").val() == "Scrap"){
            $(this).parent().nextAll(".form-group").hide();
        }
        else{
            $(this).parent().nextAll(".form-group").show();
        }
    }); 
    $(".form-close-maintenance").submit(function() {
        var result = true;
        var status = $("select[name='equipmentStatus'] option:selected").val();
        if ( status == "RFU"){
            if ( !$('input[name="operatingTime"]').val() ||
                !$('input[name="operatingTimeMin"]').val()){
                result = false;
                $(".form-close-maintenance .text-danger")
                    .text("You need to input Operating time and Minimal Operating time")
                    .show();
            }
            else if (parseInt($('input[name="operatingTime"]').val()) < 
                     parseInt($('input[name="operatingTimeMin"]').val())){
                result = false;
                $(".form-close-maintenance .text-danger")
                    .text("Operating time can't be smaller than Minimal operating time")
                    .show();
            }        
        } 
        return result;
    });
    
    $("#CreateEquipment").submit(function() {
        var validate = [
            ["department", $("select[name='department'] option:selected").val()],
            ["category", $("select[name='category'] option:selected").val()],
            ["type", $("select[name='type'] option:selected").val()]
        ];
        var result = ValidateSelects(validate);
        return result;
    });

    $("#formOpenOrder").submit(function() {
        var validate = [
            ["orderCounterparty", $("select[name='orderCounterparty'] option:selected").val()],
            ["orderContract", $("select[name='orderContract'] option:selected").val()],
            ["orderWell", $("select[name='orderWell'] option:selected").val()]
        ];
        var result = ValidateSelects(validate);
        return result;
    });

    $(".js-add-more-inputs").click(function () {
        var newUpload = document.createElement('input');
        newUpload.type = "file";
        newUpload.accept = ".jpg, .jpeg, .png, .gif";
        newUpload.name = "imgfilepath";
        $('.form-info__item-inputs').append(newUpload);
    });
    
    if ($("input[name='editDateDue']").length ){
        // console.log("IF");
        // var today = new Date().toISOString().split('T')[0];
        // console.log(today);
        // $("input[name='dueDate]").attr('min', today);
        //var today = new Date().toISOString().split('T')[0];
        var startDate = document.getElementsByName("editDateStart")[0].val();
        var startdate = new Date(startDate).toISOString().split('T')[0];
       // document.getElementsByName("startDate")[0].setAttribute('min', startDate);

        //var endDate = document.getElementsByName("contractEndDate").val();
        document.getElementsByName("editDateDue")[0].min = startdate
    }
    
    if($(".history").length){
        var i = 1;
        $('.history-pos').each( function(){
            $(this).attr("id", "history" + i);
            i++;
            $(this).simplePagination({
                first_content: '&lt;&lt;',
                previous_content: '<',
                next_content: '>',
                last_content: '>>',
                items_per_page: 7,
                number_of_visible_page_numbers: 10 // Treated as '9' (see below for explanation)
            });
        })
    }    

    if($("#nomenclature-list").length){
        $("#nomenclature-list").simplePagination({
            first_content: '&lt;&lt;',
            previous_content: '<',
            next_content: '>',
            last_content: '>>',
            items_per_page: 5,
            number_of_visible_page_numbers: 10 // Treated as '9' (see below for explanation)
        });
    }

    $(".header-nav__icon").click(function(){
        if ($(this).hasClass("menu-open")){
            $(this).removeClass("menu-open");
            $(".header-nav__container").hide();
        }
        else{
            $(this).addClass("menu-open");
            $(".header-nav__container")
            .css("display", "flex")
            .hide()
            .fadeIn(200);
        }
    });

    $(".form-info .form-info__heading").click(function(){
        $(this).next(".info-dropdown").stop().slideToggle();
    });
    
    
});

function ValidateSelects(validatearray){
    var result = true;
    for (var i = 0; i < validatearray.length; i++){
        if (validatearray[i][1] == -1){
            result = false;
            $("[name='"+validatearray[i][0]+"']").next().show();
        }
    }
    return result;
};