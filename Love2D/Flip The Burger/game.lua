local cardImages = {}
local scale = 0.5
local cardGrid = {}
local checkCard = {}

local backImage
local saveIndex = {}

local cardWidth
local cardHeight

local background = love.graphics.newImage("image/bg_tile.png") 
local backgroundImageWidth = background:getWidth() 
local backgroundImageHeight = background:getHeight() 


local showCards = false
local revealCounter = 3

local cardIndex
local x 
local y

local timer = 0

windowWidth = 950
windowHeight = 600

local game_started = false

local flipped_cards = 0
local first_index = 0
local second_index = 0
local first_array = {}
local second_array = {}

local selected_two = false


local number_array = {}
local check_duplication = {}

local correct_1D = {}
local correct_2D = {}

local isCorrect = false
local added_score = false

local check_same = 0
 
local score = 0
local total_correct = 0

local gameScene = {}

local flip_first = false

local game_finished = false

local restarted = false

local front_sfx = love.audio.newSource("sfx/flip front.wav", "static")
local back_sfx = love.audio.newSource("sfx/flip back.wav", "static")

function gameScene:load()  
  font = love.graphics.newFont("font/font.ttf", 50) 
  smallFont = love.graphics.newFont("font/font.ttf", 24) 
  tinyFont = love.graphics.newFont("font/font.ttf", 10) 

  love.window.setMode(windowWidth, windowHeight)

  backImage = love.graphics.newImage("image/back.png")
  cardImages[1] = love.graphics.newImage("image/patty.png")
  cardImages[2] = love.graphics.newImage("image/lettuce.png")
  cardImages[3] = love.graphics.newImage("image/cheese.png")
  cardImages[4] = love.graphics.newImage("image/onion.png")
  cardImages[5] = love.graphics.newImage("image/greenonion.png")
  cardImages[6] = love.graphics.newImage("image/spicy.png")
  cardImages[7] = love.graphics.newImage("image/mayo.png")
  cardImages[8] = love.graphics.newImage("image/pineapple.png")
  cardImages[9] = love.graphics.newImage("image/cutlet.png")
  cardImages[10] = love.graphics.newImage("image/drumstick.png")
  cardImages[11] = love.graphics.newImage("image/fish.png")
  cardImages[12] = love.graphics.newImage("image/lemon.png")
  cardImages[13] = love.graphics.newImage("image/pickle.png")
  cardImages[14] = love.graphics.newImage("image/shrimp.png")
  cardImages[15] = love.graphics.newImage("image/tomato.png")
  cardImages[16] = love.graphics.newImage("image/bacon.png")



  cardWidth = cardImages[1]:getWidth() * scale
  cardHeight = cardImages[1]:getHeight() * scale

  -- 1,1,2,2,....16,16과 같이 배열에 저장
  for i = 1, 16 do
    for j = 1, 2 do
      table.insert(number_array, i)
    end
  end
  
  
  flip_first = true 
  setIndex()

end

-- 1부터 32까지의 수를 랜덤하게 중복되지 않게 설정 
function setRandomValue()
  local randomVal = love.math.random(1, 32)
  local duplicated = false
    
  for i = 1, 32 do
    if randomVal == check_duplication[i] then
      duplicated = true
    end
  end
  
   
  if duplicated then
    return setRandomValue()
  else  
    table.insert(check_duplication, randomVal)    
    return randomVal
  end
end

-- 1부터 32까지 랜덤하게 설정된 번호를 1,1,2,2,.. 식으로 저장된 배열에서 가져옴 
function setIndex()
  saveIndex = {}
  for i = 1, 32 do
    cardIndex = setRandomValue()
    table.insert(saveIndex, cardIndex)
  end
end

function clearGrid()
  for i = 1, 4 do
    cardGrid[i] = {}
  end
end

-- 뒷면을 보여줌. 만약 정답으로 맞춘 카드가 있다면 저장된 행열을 가져와 해당 부분 카드는 nil로 처리
function flipBack()  
  clearGrid()
  for i = 1, 4 do
    for j = 1, 8 do      
        x = (j - 1) * (cardWidth + 15)
        y = (i - 1) * (cardHeight + 25)
        table.insert(cardGrid[i], {x = x + 30, y = y + 100, image = backImage})              
    end
  end
  print("-----------")
  print(#correct_1D,  "---", #correct_2D)
  if #correct_1D > 1 then
    for i = 1, #correct_1D do
      cardGrid[correct_1D[i]][correct_2D[i]] = nil
    end
  end

  love.audio.play(back_sfx)
end

-- 앞면을 보여줌
function flipFront()
  clearGrid()
  local count_index = 0

  for i = 1, 4 do
    for j = 1, 8 do
      count_index = count_index + 1
      x = (j - 1) * (cardWidth + 15)
      y = (i - 1) * (cardHeight + 25)
      --print(saveIndex[count_index])
      table.insert(cardGrid[i], {x = x + 30, y = y + 100, image = cardImages[number_array[saveIndex[count_index]]]})
    end
  end

  for i = 1, #correct_1D do
    for j = 1, #correct_2D do
      cardGrid[i][j] = nil
    end
  end
  love.audio.play(front_sfx)
end

-- 마우스가 클릭된 위치의 카드의 번호를 가져옴
function gameScene:mousepressed(x, y, button)
  if not game_started and button == 1 then
    showCards = true
    flipFront()
    game_started = true
  elseif game_started and button == 1 then
    local mouseX, mouseY = x, y

    for i = 1, 4 do
      for j = 1, 8 do
        if cardGrid[i][j] ~= nil then
          local index = (i-1) * 8 + j 
          local card = cardGrid[i][j]

          if mouseX >= card.x and mouseX <= card.x + cardWidth and
          mouseY >= card.y and mouseY <= card.y + cardHeight and flipped_cards < 2 and 
          ((check_same ~= 0 and check_same ~= index) or check_same == 0) and showCards == false then
            check_same = index
            flipped_cards = flipped_cards + 1
            if flipped_cards == 1 then
              first_index = number_array[saveIndex[index]]
            elseif flipped_cards == 2 then
              second_index = number_array[saveIndex[index]]
            end
            card.image = cardImages[number_array[saveIndex[index]]]
            table.insert(first_array, i)
            table.insert(second_array, j)

            local instance = front_sfx:clone()
            love.audio.play(instance)
            break
          end
        end        
      end
    end   
  end   
end


function gameScene:update(dt) 
  if flip_first then
    flipBack() 
    flip_first = false 
    restarted = false
  end


  if flipped_cards == 2 then
    selected_two = true
  end

  if selected_two == true then
    if first_index == second_index and first_index ~= 0 then
      if not added_score then
        score = score + 50
        total_correct = total_correct + 1
      end
      added_score = true        
      isCorrect = true
      table.insert(correct_1D, first_array[1])
      table.insert(correct_1D, first_array[2])
      table.insert(correct_2D, second_array[1])
      table.insert(correct_2D, second_array[2])
    else
      if not added_score then
        score = score - 10
      end
      added_score = true     
    end
      
    showCards = true
    countTime(dt, 1)
    if not showCards then
      if isCorrect then
        cardGrid[first_array[1]][second_array[1]] = nil
        cardGrid[first_array[2]][second_array[2]] = nil
      end        
      flipped_cards = 0
      first_array ={}
      second_array = {}    
      first_array ={}
      second_array = {}
      first_index = 0
      second_index = 0    
      check_same = 0
      added_score = false
      selected_two = false
      isCorrect = false
    end
  end

 
  -- 게임 시작 시 뒷면 보여줌
  if showCards == true and not selected_two then
    countTime(dt, revealCounter)
  end

  if total_correct == 16 then
    game_finished = true
  end
end

-- 매개변수로 받은 시간만큼 흐른 뒤 뒷면으로 다시 뒤집음
function countTime(delta, max_time)
  timer = timer + delta
  if timer > max_time then      
    if not game_started then
      game_started = true
    end
    showCards = false
    timer = 0
    return flipBack()
  end
end


function gameScene:draw()
   drawBackground()

   love.graphics.setColor(0, 0, 0)
   love.graphics.setFont(font)
   local socre_txt = score
   love.graphics.print(score, windowWidth/2 - font:getWidth(socre_txt)/2, 0)
   
   if not game_finished then
    love.graphics.setFont(tinyFont)
    love.graphics.print("press R to restart the game", 5, 10)
   end
  
 
  -- 화면에 카드 배치
   love.graphics.setColor(1, 1, 1)
   for i = 1, 4 do
     for j = 1, 8 do     
       if cardGrid[i][j] ~= nil then
        love.graphics.draw(cardGrid[i][j].image, cardGrid[i][j].x, cardGrid[i][j].y, 0, scale, scale)
       end       
     end
  end

  -- 게임 완료 텍스트 표시
  if game_finished then
    love.graphics.setColor(0, 0, 0)
    love.graphics.setFont(font)
    congrats_txt = "Congrats!"
    love.graphics.print(congrats_txt, windowWidth/2 - font:getWidth(congrats_txt)/2, windowHeight/2 - font:getHeight(congrats_txt)/2)
    love.graphics.setFont(smallFont)
    restart_txt = "Press any key to restart"
    love.graphics.print(restart_txt, windowWidth/2 - smallFont:getWidth(restart_txt)/2, windowHeight/2 - smallFont:getHeight(restart_txt)/2 + 200)
  end

  love.graphics.setColor(1,0,0)


end

function drawBackground()
  love.graphics.setColor(1, 1, 1)
  for y = 0, love.graphics.getHeight(), backgroundImageHeight do
    for x = 0, love.graphics.getWidth(), backgroundImageWidth do
        love.graphics.draw(background, x, y)
    end
  end
end


function gameScene:keypressed(key)
  if not restarted and (game_finished or key == "r")then
    reset()
    setIndex()
    restarted = true
  end
end

-- 게임 재시작을 위한 값들 초기화
function reset()
  print("restart")
  flip_first = true 
  showCards = false
  timer = 0
  game_started = false
  flipped_cards = 0
  first_index = 0
  second_index = 0
  first_array = {}
  second_array = {}
  selected_two = false
  check_duplication = {}
  correct_1D = {}
  correct_2D = {}
  isCorrect = false
  added_score = false
  check_same = 0
  score = 0
  total_correct = 0
  game_finished = false
end

return gameScene