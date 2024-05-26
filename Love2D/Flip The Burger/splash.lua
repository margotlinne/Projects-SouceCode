local splashScene = {}
local intervals = 0.3
local timer = 0

local timer_to_game = 0
local max_time = 3

game_starts = false

local burble_sfx = love.audio.newSource("sfx/burbles.wav", "static")
local sfx_instance = burble_sfx:clone()

function splashScene:load()
    splash_image = love.graphics.newImage("image/made with love(ver2).png")
    splash_imageR = love.graphics.newImage("image/made with love_r(ver2).png")
    splash_imageL = love.graphics.newImage("image/made with love_l(ver2).png")

    current_image = splash_image
    previous_image = nil
    
    love.audio.play(sfx_instance)
end

function splashScene:update(dt)
    timer = timer + dt
    timer_to_game = timer_to_game + dt

    if timer > intervals then
        if current_image == splash_image then
            if previous_image == nil or previous_image == splash_imageL then
                current_image = splash_imageR
            elseif previous_image == splash_imageR then
                current_image = splash_imageL
            end
        elseif current_image == splash_imageR then
            previous_image = splash_imageR
            current_image = splash_image
        else 
            previous_image = splash_imageL
            current_image = splash_image
        end
        
        timer = 0
    end

    if timer_to_game > max_time then
        game_starts = true 
    end
end

function splashScene:draw()
    love.graphics.clear(60/255, 57/255, 44/255)
   -- print(current_image:getWidth())
    love.graphics.draw(current_image, windowWidth/2 - current_image:getWidth() * 0.3 / 2, 
        windowHeight/2 - current_image:getHeight() * 0.3 / 2, 0, 0.3, 0.3)
        
   
end

return splashScene