
windowWidth = 950
windowHeight = 600


function love.load()  
  love.window.setMode(windowWidth, windowHeight)

  splashScene = require("scene/splash")
  gameScene = require("scene/game")

  if splashScene.load ~= nil then
    splashScene:load()
  end
  if gameScene.load ~= nil then 
    gameScene:load()
  end

end

function love.keypressed(key)
  if gameScene.keypressed ~= nil and game_starts then
    gameScene:keypressed(key)
  end
end

function love.mousepressed(x, y, button)
  if gameScene.mousepressed ~= nil and game_starts then
    gameScene:mousepressed(x, y, button)
  end
end

function love.update(dt) 
  if splashScene.update ~= nil and not game_starts then
    splashScene:update(dt)
  end

  if gameScene.update ~= nil and game_starts then
    gameScene:update(dt)
  end
  
 
end

function love.draw()
  if splashScene.draw ~= nil and not game_starts then
    splashScene:draw()
  end

  if gameScene.draw ~= nil and game_starts then
    gameScene:draw()
  end
end
