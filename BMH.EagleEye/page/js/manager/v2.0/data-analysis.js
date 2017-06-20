(function(){
    function autoHeight(){
        var winh = $(window).height() - 105;
        $('#YY-main').height(winh);
        $('.tableBody').height(winh - 60-60-49 - 70 - 15)
    }
    autoHeight();

    function leftSideDown(){
        $('.leftSide').find('li>a').click(function(){
                $(this).next('dl').stop().slideToggle()
                if($(this).parent().hasClass('off')){
                    $(this).parent().removeClass('off')
                }else{
                    $(this).parent().addClass('off')
                }
        })
    }
    leftSideDown();
    function selectBox(){
            var triangle = $('.slidebox').find('.sanjiao');
            var $w;
            $('.slidebox .box').each(function(){
                $w = $(this).outerWidth();
                $(this).next().width($w)

            })
            $('.slideAd .slide,.slideChannel .slide').width(223)
            $('.slidebox').on('click', '.box', function(e) {
                var parent = $(this).closest('.slidebox');
                if (!parent.hasClass('is-open')) {
                    $(this).next().slideDown(300);
                    parent.addClass('is-open')
                    $(this).find('.triangle').addClass('open');
                } else {
                    $(this).next().slideUp(300);
                    parent.removeClass('is-open');
                    $(this).find('.triangle').removeClass('open');
                }
                e.stopPropagation();
             })
            $('.slidebox').on('click', '.slide', function(e) {
                e.stopPropagation();
            })
            //点击body隐藏
            $('html').on('click', function(e) {
                $('.slide').slideUp(300);
                $('.slidebox').removeClass('is-open');
                $(this).find('.triangle').removeClass('open');
                
            });
            $('.slide dd').click(function(){
                var $txt = $(this).find("em").text();
                $(this).parents('.slide').prev().find('.title').text($txt);
                $(this).parents('.slide').slideUp();
                $(this).parents('.slidebox').removeClass('is-open').find('.triangle').removeClass('open');
                $(this).parents('.slidebox').find('.title').removeClass('gray');
                $(this).parents('.slidebox').removeClass('is-open');
            })
        $('.data-analysis .slide td').click(function(event){
            
            var label = $(this).find('label'),that = $(this),_choicehtml='',_choicetxt ='';
            if(label.hasClass('cur')){
                label.removeClass('cur')
                
            }else{
                label.addClass('cur')               
            } 
            that.parents('.slide').find('.cur').each(function(){
               _choicetxt += $(this).parent().next('em').text() + '  ';
            });
           // that.parents('.slidebox').find('.title').removeClass('gray')
            //that.parents('.slidebox').find('.choice .title').text(_choicetxt);
            if(that.parents('.slidebox').find('.choice .title').text() == ''){
                that.parents('.slideAd').find('.choice .title').html('广告位<em>（可多选）</em>')
                that.parents('.slideChannel').find('.choice .title').html('频道分类<em>（可多选）</em>')
                 that.parents('.slidebox').find('.title').addClass('gray')
            }


         event.stopPropagation()
        })
    }
    selectBox();

    $(".tableBody").niceScroll({  //调用滚动条
      cursorcolor:"#12bdce", 
      cursoropacitymin:1, 
      cursoropacitymax:1,  
      touchbehavior:true,  
      cursorwidth:"5px",  
      cursorborder:"0",  
      cursorborderradius:"5px",
      horizrailenabled:false  
    });





})()

 
    

