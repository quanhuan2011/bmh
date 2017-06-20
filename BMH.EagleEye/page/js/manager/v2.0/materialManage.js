(function(){
     $(".new").click(function(){
                    yyCommon.popoupShow();
     });
    $(".loginmask,.confirm,.cancel").click(function(){
                    yyCommon.popoupHide();
    });
    $('.editBox a').eq(0).click(function(){
            yyCommon.popoupShow();
     })

    $(".popup .tab-bd,.tableBody").niceScroll({  //调用滚动条
      cursorcolor:"#12bdce", 
      cursoropacitymin:1, 
      cursoropacitymax:1,  
      touchbehavior:true,  
      cursorwidth:"5px",  
      cursorborder:"0",  
      cursorborderradius:"5px",
      horizrailenabled:false  
    });

    $('.editBox .del').click(function(){
        $(this).parents('tr').remove()
        return false;
    }) 
    $('.edit').each(function(){
      var $h = $(this).parents('td').outerHeight()-1;
      $(this).height($h).find('a').css('lineHeight',$h+'px')
    })





})()

 
    

