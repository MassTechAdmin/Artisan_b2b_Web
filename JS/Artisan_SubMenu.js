        // ****************************************************
        // 雙國
        // ****************************************************
        $(document).ready(function()
        {
            $(".account").click(function()
            {
                var X=$(this).attr('id');

                if(X==1)
                {
                    $(".submenu").hide();
                    $(this).attr('id', '0');	
                }
                else
                {
                    $(".submenu").show();
                    $(this).attr('id', '1');
                }
            });

            //Mouseup textarea false
            $(".submenu").mouseup(function()
            {
                return false
            });
            $(".account").mouseup(function()
            {
                return false
            });

            //Textarea without editing.
            $(document).mouseup(function()
            {
                $(".submenu").hide();
                $(".account").attr('id', '');
            });
        });
        
        
        // ****************************************************
        // 多國
        // ****************************************************
        $(document).ready(function()
        {
            $(".account2").click(function()
            {
                var X=$(this).attr('id');

                if(X==1)
                {
                    $(".submenu2").hide();
                    $(this).attr('id', '0');	
                }
                else
                {
                    $(".submenu2").show();
                    $(this).attr('id', '1');
                }
            });

            //Mouseup textarea false
            $(".submenu2").mouseup(function()
            {
                return false
            });
            $(".account2").mouseup(function()
            {
                return false
            });

            //Textarea without editing.
            $(document).mouseup(function()
            {
                $(".submenu2").hide();
                $(".account2").attr('id', '');
            });
        });
        
        
        // ****************************************************
        // 行程搜尋
        // ****************************************************
        $(document).ready(function(){
	        $(".mainNav a").mouseover(function(){
		        $(".mainNav a").attr("class","menu_link");
		        $("#"+this.id).attr("class","actived menu_link");
		        var currentMenuNo = parseInt(this.id.substring(1));
		        $(".secondNav div").each(function(){
			        $(this).hide();
			        $("#subNav"+currentMenuNo).show();
		        });
	        });
        });