SET IDENTITY_INSERT Quizzes ON;
-- Insert Quizzes
INSERT INTO Quizzes (Id, Title, Description, InsertedAt, ModifiedAt)
VALUES
(1, 'Science Quiz', 'Science Quiz for enthusiasts!', GETDATE(), GETDATE()),
(2, 'History Quiz', 'History Quiz for enthusiasts!', GETDATE(), GETDATE()),
(3, 'Geography Quiz', 'Geography Quiz for enthusiasts!', GETDATE(), GETDATE()),
(4, 'Mathematics Quiz', 'Mathematics Quiz for enthusiasts!', GETDATE(), GETDATE()),
(5, 'Technology Quiz', 'Technology Quiz for enthusiasts!', GETDATE(), GETDATE()),
(6, 'Gaming Quiz', 'Gaming Quiz for enthusiasts!', GETDATE(), GETDATE()),
(7, 'Dictionary Quiz', 'Dictionary Quiz for enthusiasts!', GETDATE(), GETDATE()),
(8, 'Sports Quiz', 'Sports Quiz for enthusiasts!', GETDATE(), GETDATE()),
(9, 'Food and Drink Quiz', 'Food and Drink Quiz for enthusiasts!', GETDATE(), GETDATE()),
(10, 'Zoology Quiz', 'Zoology Quiz for enthusiasts!', GETDATE(), GETDATE()),
(11, 'Cinema Quiz', 'Cinema Quiz for enthusiasts!', GETDATE(), GETDATE()),
(12, 'Music Quiz', 'Music Quiz for enthusiasts!', GETDATE(), GETDATE());

-- Define Quiz IDs

DECLARE @ScienceQuizId INT = 1;

DECLARE @HistoryQuizId INT = 2;

DECLARE @GeographyQuizId INT = 3;

DECLARE @MathematicsQuizId INT = 4;

DECLARE @TechnologyQuizId INT = 5;

DECLARE @GamingQuizId INT = 6;

DECLARE @DictionaryQuizId INT = 7;

DECLARE @SportsQuizId INT = 8;

DECLARE @FoodandDrinkQuizId INT = 9;

DECLARE @ZoologyQuizId INT = 10;

DECLARE @CinemaQuizId INT = 11;

DECLARE @MusicQuizId INT = 12;


-- Insert Quizzes

INSERT INTO Quizzes (Id, Title, Description, InsertedAt, ModifiedAt)
VALUES

(@ScienceQuizId, 'Science Quiz', 'Science Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@HistoryQuizId, 'History Quiz', 'History Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@GeographyQuizId, 'Geography Quiz', 'Geography Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@MathematicsQuizId, 'Mathematics Quiz', 'Mathematics Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@TechnologyQuizId, 'Technology Quiz', 'Technology Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@GamingQuizId, 'Gaming Quiz', 'Gaming Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@DictionaryQuizId, 'Dictionary Quiz', 'Dictionary Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@SportsQuizId, 'Sports Quiz', 'Sports Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@FoodandDrinkQuizId, 'Food and Drink Quiz', 'Food and Drink Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@ZoologyQuizId, 'Zoology Quiz', 'Zoology Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@CinemaQuizId, 'Cinema Quiz', 'Cinema Quiz for enthusiasts!', GETDATE(), GETDATE()),
(@MusicQuizId, 'Music Quiz', 'Music Quiz for enthusiasts!', GETDATE(), GETDATE());


-- Insert Questions for Science Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('What is the chemical symbol for sodium?', 'Sn,So,Na,Ne', 'Na', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the powerhouse of the cell?', 'Nucleus,Mitochondria,Ribosome,Golgi Apparatus', 'Mitochondria', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the pH of pure water?', '5,7,9,11', '7', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the heaviest naturally occurring element?', 'Uranium,Plutonium,Lead,Gold', 'Uranium', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the speed of light in a vacuum?', '299792458 m/s,300000000 m/s,150000000 m/s,299792000 m/s', '299792458 m/s', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What type of bond exists between hydrogen and oxygen in a water molecule?', 'Ionic,Covalent,Metallic,Hydrogen', 'Covalent', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the most abundant gas in the Earth’s atmosphere?', 'Oxygen,Nitrogen,Carbon Dioxide,Hydrogen', 'Nitrogen', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the SI unit of electric current?', 'Volt,Ohm,Ampere,Watt', 'Ampere', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('Which planet is the hottest in the solar system?', 'Mercury,Mars,Venus,Jupiter', 'Venus', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the main component of the Sun?', 'Nitrogen,Helium,Oxygen,Hydrogen', 'Hydrogen', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the primary function of the large intestine?', 'Absorption of nutrients,Digestion of food,Absorption of water,Production of bile', 'Absorption of water', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the second most abundant element in the Earth’s crust?', 'Oxygen,Silicon,Aluminum,Iron', 'Silicon', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the boiling point of water at sea level in Celsius?', '90,100,110,120', '100', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What type of energy is stored in a stretched spring?', 'Kinetic,Chemical,Thermal,Potential', 'Potential', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the common name for dihydrogen monoxide?', 'Ammonia,Hydrogen Peroxide,Methane,Water', 'Water', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What does DNA stand for?', 'Deoxyribonucleic Acid,Deoxyribosomal Acid,Dioxyribonucleic Acid,Deoxyribonuclide Acid', 'Deoxyribonucleic Acid', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('Which particle is negatively charged?', 'Proton,Electron,Neutron,Photon', 'Electron', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the freezing point of water in Fahrenheit?', '0,212,32,100', '32', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What gas do plants absorb during photosynthesis?', 'Oxygen,Nitrogen,Carbon Dioxide,Hydrogen', 'Carbon Dioxide', 'Science', @ScienceQuizId, GETDATE(), GETDATE()),
('What is the chemical formula for table salt?', 'NaBr,KCl,CaCl,NaCl', 'NaCl', 'Science', @ScienceQuizId, GETDATE(), GETDATE());

-- Insert Questions for History Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('In which year did World War II begin?', '1914,1939,1940,1945', '1939', 'History', 2, GETDATE(), GETDATE()),
('Who was the first emperor of Rome?', 'Nero,Augustus,Caesar,Tiberius', 'Augustus', 'History', 2, GETDATE(), GETDATE()),
('What was the name of the ship that carried the Pilgrims to America in 1620?', 'Discovery,Mayflower,Santa Maria,Pinta', 'Mayflower', 'History', 2, GETDATE(), GETDATE()),
('Which country was known as Persia until 1935?', 'Iraq,Egypt,Syria,Iran', 'Iran', 'History', 2, GETDATE(), GETDATE()),
('Who was the first president of the United States?', 'George Washington,Thomas Jefferson,John Adams,Abraham Lincoln', 'George Washington', 'History', 2, GETDATE(), GETDATE()),
('What ancient civilization built Machu Picchu?', 'Mayans,Olmecs,Aztecs,Incas', 'Incas', 'History', 2, GETDATE(), GETDATE()),
('Who discovered America?', 'Christopher Columbus,Vasco da Gama,Amerigo Vespucci,Ferdinand Magellan', 'Christopher Columbus', 'History', 2, GETDATE(), GETDATE()),
('What year did the Titanic sink?', '1909,1910,1911,1912', '1912', 'History', 2, GETDATE(), GETDATE()),
('Which empire was known for its use of chariots?', 'Assyrian,Hittite,Babylonian,Persian', 'Hittite', 'History', 2, GETDATE(), GETDATE()),
('Who was the British prime minister during World War II?', 'Winston Churchill,Neville Chamberlain,David Lloyd George,Harold Macmillan', 'Winston Churchill', 'History', 2, GETDATE(), GETDATE()),
('Who was known as the "Sun King"?', 'Philip II,Henry VIII,Charles V,Louis XIV', 'Louis XIV', 'History', 2, GETDATE(), GETDATE()),
('In what year did the Berlin Wall fall?', '1987,1989,1990,1991', '1989', 'History', 2, GETDATE(), GETDATE()),
('Who led the Soviet Union during World War II?', 'Putin,Lenin,Khrushchev,Stalin', 'Stalin', 'History', 2, GETDATE(), GETDATE()),
('What war was fought between the north and south regions of the United States?', 'World War I,Civil War,Revolutionary War,Korean War', 'Civil War', 'History', 2, GETDATE(), GETDATE()),
('What dynasty ruled China for over 400 years starting in 206 BC?', 'Song,Qing,Han,Ming', 'Han', 'History', 2, GETDATE(), GETDATE()),
('Who was the Greek god of war?', 'Zeus,Hades,Ares,Poseidon', 'Ares', 'History', 2, GETDATE(), GETDATE()),
('What was the primary language of the Roman Empire?', 'Latin,Greek,Italian,Spanish', 'Latin', 'History', 2, GETDATE(), GETDATE()),
('Who painted the ceiling of the Sistine Chapel?', 'Raphael,Leonardo da Vinci,Michelangelo,Donatello', 'Michelangelo', 'History', 2, GETDATE(), GETDATE()),
('What year did the French Revolution begin?', '1776,1781,1789,1804', '1789', 'History', 2, GETDATE(), GETDATE()),
('What was the capital of the Byzantine Empire?', 'Rome,Constantinople,Athens,Alexandria', 'Constantinople', 'History', 2, GETDATE(), GETDATE());

-- Insert Questions for Geography Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('What is the largest desert in the world?', 'Sahara,Arctic,Antarctic,Gobi', 'Antarctic', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the capital of Canada?', 'Toronto,Vancouver,Ottawa,Montreal', 'Ottawa', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which river is the longest in the world?', 'Amazon,Mississippi,Nile,Yangtze', 'Nile', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What country has the most islands?', 'Norway,Sweden,Canada,Indonesia', 'Sweden', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What mountain range separates Europe from Asia?', 'Himalayas,Andes,Rockies,Ural', 'Ural', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the smallest country in the world?', 'Monaco,Liechtenstein,Vatican City,Malta', 'Vatican City', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the capital of Australia?', 'Sydney,Melbourne,Canberra,Brisbane', 'Canberra', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which continent is the driest?', 'Antarctica,Africa,Asia,Australia', 'Antarctica', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the largest ocean on Earth?', 'Atlantic,Indian,Pacific,Arctic', 'Pacific', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which country has the highest population?', 'China,India,USA,Indonesia', 'China', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the capital of South Africa?', 'Cape Town,Pretoria,Bloemfontein,All of the above', 'All of the above', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which country is known as the Land of the Rising Sun?', 'China,Japan,South Korea,Vietnam', 'Japan', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the name of the longest mountain range on Earth?', 'Andes,Himalayas,Rockies,Alps', 'Andes', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the capital of Brazil?', 'Rio de Janeiro,Brasilia,Sao Paulo,Salvador', 'Brasilia', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which desert covers much of northern Africa?', 'Gobi,Sahara,Atacama,Kalahari', 'Sahara', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which US state is the largest by area?', 'Texas,California,Alaska,Montana', 'Alaska', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which European city is known as the City of Canals?', 'Venice,Amsterdam,Bruges,Stockholm', 'Venice', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the deepest point in the ocean?', 'Mariana Trench,Tonga Trench,Kermadec Trench,Java Trench', 'Mariana Trench', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('Which country is home to the Great Barrier Reef?', 'Australia,USA,South Africa,India', 'Australia', 'Geography', @GeographyQuizId, GETDATE(), GETDATE()),
('What is the official language of Brazil?', 'Spanish,French,Portuguese,English', 'Portuguese', 'Geography', @GeographyQuizId, GETDATE(), GETDATE());

-- Insert Questions for Mathematics Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('What is the value of Ï€ (pi) to 5 decimal places?', '3.14159,3.14286,3.14126,3.14142', '3.14159', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is 12 factorial (12!)?', '479001600,39916800,6227020800,622702080', '479001600', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the square root of 256?', '14,15,16,17', '16', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is 5 to the power of 3?', '25,125,15,225', '125', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the derivative of sin(x)?', 'sin(x),cos(x),-cos(x),-sin(x)', 'cos(x)', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the sum of angles in a triangle?', '180°,360°,90°,270°', '180°', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the integral of 2x?', 'x^2 + C,2x^2 + C,2x + C,x + C', 'x^2 + C', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the smallest prime number?', '1,2,3,0', '2', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the mode of the dataset {3, 5, 6, 3, 7}?', '3,5,6,7', '3', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the median of the dataset {4, 8, 15, 16, 23, 42}?', '15,16,19,23', '15', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the area of a circle with radius 7?', '44,49,154,176', '154', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is 5% of 200?', '10,15,22,25', '10', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the volume of a cube with edge length 3?', '9,18,27,81', '27', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the solution to the equation x + 5 = 12?', '6,7,5,12', '7', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the Pythagorean Theorem?', 'a^2 + b^2 = c^2,a^2 + c^2 = b^2,b^2 + c^2 = a^2,c^2 = a^2 - b^2', 'a^2 + b^2 = c^2', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is 100 divided by 4?', '20,25,30,40', '25', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the sum of the first 10 natural numbers?', '45,50,55,60', '55', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the decimal equivalent of 1/4?', '0.25,0.5,0.125,0.75', '0.25', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the circumference of a circle with diameter 14?', '44,28,38,22', '44', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE()),
('What is the solution to the quadratic equation x^2 - 4 = 0?', '2,-2,Both,Neither', 'Both', 'Mathematics', @MathematicsQuizId, GETDATE(), GETDATE());

-- Insert Questions for Technology Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('What does CPU stand for?', 'Control Processing Unit,Computer Processing Unit,Central Processing Unit,Central Power Unit', 'Central Processing Unit', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What is the name of the first electronic general-purpose computer?', 'UNIVAC,ENIAC,Colossus,ABC', 'ENIAC', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What programming language is primarily used for iOS development?', 'Java,C#,Swift,Python', 'Swift', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What does HTML stand for?', 'HyperTransfer Markup Language,HyperText Markup Language,HyperText Machine Language,HyperText Management Language', 'HyperText Markup Language', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('Who is known as the father of computing?', 'Alan Turing,Bill Gates,John von Neumann,Charles Babbage', 'Charles Babbage', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What does RAM stand for?', 'Randomized Access Memory,Read Access Memory,Random Access Memory,Read And Memory', 'Random Access Memory', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('Which company developed the Android operating system?', 'Apple,Google,Microsoft,Samsung', 'Google', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What is the name of the first web browser?', 'Internet Explorer,Mosaic,WorldWideWeb,Netscape Navigator', 'WorldWideWeb', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What is the smallest unit of data in a computer?', 'Nibble,Bit,Byte,Word', 'Bit', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('Which programming language is used to develop Android apps?', 'Swift,C#,Java,Python', 'Java', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What does IoT stand for?', 'Integration of Technology,Internet over Technology,Internet of Things,Internet of Technology', 'Internet of Things', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What is the primary protocol used for sending emails?', 'FTP,HTTP,SMTP,POP3', 'SMTP', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('Which company developed the Windows operating system?', 'Apple,IBM,Microsoft,Google', 'Microsoft', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What does VPN stand for?', 'Virtual Public Network,Virtual Personal Network,Virtual Private Network,Virtual Private Node', 'Virtual Private Network', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What does AI stand for?', 'Automated Intelligence,Advanced Interface,Artificial Intelligence,Adaptive Intelligence', 'Artificial Intelligence', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('Which device is used to connect a computer to a network?', 'Monitor,Router,CPU,Printer', 'Router', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What does HTTP stand for?', 'Hyper Transfer Text Protocol,HyperText Transmission Protocol,HyperText Transfer Protocol,Hyper Transfer Technology Protocol', 'HyperText Transfer Protocol', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What is the main function of a firewall?', 'Enhance speed,Improve graphics,Prevent unauthorized access,Backup data', 'Prevent unauthorized access', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('Which programming language is often used for web development alongside HTML and CSS?', 'Python,C++,Ruby,JavaScript', 'JavaScript', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE()),
('What is the name of the world’s most popular search engine?', 'Yahoo,Google,DuckDuckGo,Bing', 'Google', 'Technology', @TechnologyQuizId, GETDATE(), GETDATE());

-- Insert Questions for Gaming Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('What is the best-selling video game of all time?', 'GTA V,Tetris,Call of Duty,Minecraft', 'Minecraft', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('In which year was the PlayStation 1 released?', '1995,1996,1994,1993', '1994', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What does FPS stand for in gaming?', 'First Player Shooter,Fighting Player System,First Person Shooter,Frames Per Second', 'First Person Shooter', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What is the main currency in Fortnite?', 'Coins,V-Bucks,Gems,Robux', 'V-Bucks', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which company developed the Legend of Zelda series?', 'Sony,Nintendo,Microsoft,EA', 'Nintendo', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What is the highest rank in Counter-Strike: Global Offensive?', 'Master Guardian,Global Elite,Legendary Eagle,Silver Elite', 'Global Elite', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which game features a character named Geralt of Rivia?', 'Dark Souls,Dragon Age,The Witcher,Elden Ring', 'The Witcher', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What does NPC stand for?', 'New Player Control,Next Player Character,Non-Player Competitor,Non-Playable Character', 'Non-Playable Character', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which game is set in the fictional city of Los Santos?', 'Watch Dogs,Saints Row,GTA V,Red Dead Redemption', 'GTA V', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What is the name of the main character in the Halo series?', 'Gordon Freeman,Solid Snake,Luke Skywalker,Master Chief', 'Master Chief', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which game popularized the battle royale genre?', 'Fortnite,PUBG,Apex Legends,Warzone', 'PUBG', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What is the name of the platformer game featuring a plumber as the protagonist?', 'Donkey Kong,Pac-Man,Sonic the Hedgehog,Super Mario Bros', 'Super Mario Bros', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('In which year was the Xbox released?', '2000,1999,2001,2002', '2001', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which game series includes the subtitle "Assassin of Kings"?', 'Dragon Age,Elder Scrolls,The Witcher,Dark Souls', 'The Witcher', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What is the main objective in Pac-Man?', 'Defeat the ghosts,Collect coins,Unlock levels,Eat all the dots', 'Eat all the dots', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which company developed World of Warcraft?', 'Ubisoft,Riot Games,EA Sports,Blizzard Entertainment', 'Blizzard Entertainment', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What is the name of the famous fighting game series featuring characters like Ryu and Ken?', 'Tekken,King of Fighters,Mortal Kombat,Street Fighter', 'Street Fighter', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which game involves exploring randomly generated worlds and crafting tools and structures?', 'Roblox,Minecraft,No Man’s Sky,Terraria', 'Minecraft', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('What is the name of the protagonist in The Last of Us?', 'Ethan,Arthur,Joel,Marcus', 'Joel', 'Gaming', @GamingQuizId, GETDATE(), GETDATE()),
('Which game features the map "Dust II"?', 'Valorant,Call of Duty,Battlefield,Counter-Strike', 'Counter-Strike', 'Gaming', @GamingQuizId, GETDATE(), GETDATE());

-- Insert Questions for Dictionary Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('What is the meaning of "ephemeral"?', 'Everlasting,Complicated,Unusual,Temporary', 'Temporary', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is a synonym for "benevolent"?', 'Mean,Kind,Angry,Lazy', 'Kind', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What does "audacious" mean?', 'Fearful,Bold,Reserved,Shy', 'Bold', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the antonym of "lucid"?', 'Transparent,Evident,Clear,Confused', 'Confused', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What does "gregarious" mean?', 'Quiet,Isolated,Sociable,Shy', 'Sociable', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the meaning of "ubiquitous"?', 'Absent,Hidden,Rare,Omnipresent', 'Omnipresent', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is a synonym for "euphoria"?', 'Anger,Sadness,Happiness,Fear', 'Happiness', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What does "antithesis" mean?', 'Plan,Example,Opposite,Similarity', 'Opposite', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the antonym of "candid"?', 'Open,Blunt,Honest,Deceptive', 'Deceptive', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What does "pragmatic" mean?', 'Imaginative,Illogical,Practical,Abstract', 'Practical', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is a synonym for "meticulous"?', 'Lazy,Rushed,Careless,Detailed', 'Detailed', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What does "ambiguous" mean?', 'Definite,Transparent,Unclear,Obvious', 'Unclear', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the meaning of "belligerent"?', 'Calm,Friendly,Shy,Hostile', 'Hostile', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the antonym of "arduous"?', 'Difficult,Exhausting,Easy,Complex', 'Easy', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What does "juxtapose" mean?', 'Separate,Combine,Place side by side,Ignore', 'Place side by side', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the meaning of "ameliorate"?', 'Worsen,Improve,Ignore,Damage', 'Improve', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is a synonym for "esoteric"?', 'Common,Obscure,Known,Obvious', 'Obscure', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What does "voracious" mean?', 'Tired,Uninterested,Satisfied,Greedy', 'Greedy', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the antonym of "apathetic"?', 'Indifferent,Interested,Disengaged,Unconcerned', 'Interested', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE()),
('What is the meaning of "plethora"?', 'Shortage,Lack,Scarcity,Excess', 'Excess', 'Dictionary', @DictionaryQuizId, GETDATE(), GETDATE());

-- Insert Questions for Sports Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('Which country has won the most FIFA World Cups?', 'Germany,Argentina,Brazil,Italy', 'Brazil', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('How many players are there in a rugby team?', '13,12,11,15', '15', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the national sport of Japan?', 'Judo,Baseball,Karate,Sumo Wrestling', 'Sumo Wrestling', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Which tennis tournament is played on grass courts?', 'French Open,Australian Open,US Open,Wimbledon', 'Wimbledon', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Which country hosted the 2016 Summer Olympics?', 'China,Russia,Japan,Brazil', 'Brazil', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the length of a marathon?', '40 km,26.195 km,42.195 km,50 km', '42.195 km', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('In which sport would you perform a slam dunk?', 'Football,Tennis,Volleyball,Basketball', 'Basketball', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Which cricket player is known as the "God of Cricket"?', 'Virat Kohli,Ricky Ponting,Sachin Tendulkar,MS Dhoni', 'Sachin Tendulkar', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('How many gold medals did Michael Phelps win in the 2008 Olympics?', '6,7,8,9', '8', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the highest governing body of football (soccer)?', 'UEFA,AFC,FIFA,NFL', 'FIFA', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the term for scoring three goals in a single game of football?', 'Triple Play,Triathlon,Hat Trick,Threepeat', 'Hat Trick', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Which sport uses the terms "spike" and "block"?', 'Table Tennis,Tennis,Badminton,Volleyball', 'Volleyball', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Which country won the first Cricket World Cup?', 'Australia,India,West Indies,England', 'West Indies', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the maximum score possible in a single frame of bowling?', '150,200,250,300', '300', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the diameter of a basketball hoop in inches?', '16,20,24,18', '18', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Which country is known as the birthplace of golf?', 'Ireland,Scotland,USA,England', 'Scotland', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the length of an Olympic swimming pool?', '25 meters,100 meters,75 meters,50 meters', '50 meters', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Who holds the record for the fastest 100m sprint?', 'Carl Lewis,Tyson Gay,Yohan Blake,Usain Bolt', 'Usain Bolt', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('Which sport is played with a shuttlecock?', 'Tennis,Table Tennis,Squash,Badminton', 'Badminton', 'Sports', @SportsQuizId, GETDATE(), GETDATE()),
('What is the term for zero points in tennis?', 'Deuce,Advantage,Fault,Love', 'Love', 'Sports', @SportsQuizId, GETDATE(), GETDATE());


-- Insert Questions for Food and Drink Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('Which fruit is known as the "king of fruits"?', 'Apple,Durian,Mango,Banana', 'Mango', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is the main ingredient in guacamole?', 'Tomato,Avocado,Lime,Cilantro', 'Avocado', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which type of pasta is shaped like a bow tie?', 'Spaghetti,Macaroni,Fusilli,Farfalle', 'Farfalle', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is the national dish of Japan?', 'Sashimi,Tempura,Ramen,Sushi', 'Sushi', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What type of food is Manchego?', 'Bread,Cheese,Fruit,Meat', 'Cheese', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which country is famous for paella?', 'Italy,Mexico,Greece,Spain', 'Spain', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is tofu made from?', 'Corn,Wheat,Soybeans,Rice', 'Soybeans', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which drink is known as "liquid bread"?', 'Milkshake,Beer,Wine,Smoothie', 'Beer', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What spice is derived from dried flower buds?', 'Nutmeg,Saffron,Clove,Cinnamon', 'Clove', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which fruit has varieties called Cavendish and Plantain?', 'Mango,Apple,Banana,Pear', 'Banana', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is the main ingredient in hummus?', 'Lentils,Chickpeas,Peas,Beans', 'Chickpeas', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What type of tea is fermented?', 'Green tea,Herbal tea,White tea,Black tea', 'Black tea', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which country is known for its maple syrup?', 'France,Australia,USA,Canada', 'Canada', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is the key ingredient in pesto?', 'Cilantro,Parsley,Basil,Thyme', 'Basil', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is the main ingredient in miso soup?', 'Chicken stock,Rice,Vinegar,Soybean paste', 'Soybean paste', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is the most consumed beverage in the world after water?', 'Juice,Tea,Soda,Coffee', 'Tea', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which type of sugar is found in milk?', 'Fructose,Sucrose,Lactose,Glucose', 'Lactose', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which fruit is used to make wine?', 'Apple,Peach,Pear,Grape', 'Grape', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('What is the Italian word for "pie"?', 'Focaccia,Calzone,Tiramisu,Pizza', 'Pizza', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE()),
('Which nut is used to make marzipan?', 'Cashew,Pecan,Hazelnut,Almond', 'Almond', 'Food & Drink', @FoodandDrinkQuizId, GETDATE(), GETDATE());


-- Insert Questions for Zoology Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('What is the largest mammal in the world?', 'Elephant,Hippopotamus,Blue Whale,Giraffe', 'Blue Whale', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('Which bird is known for its colorful tail feathers?', 'Parrot,Peacock,Ostrich,Flamingo', 'Peacock', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the fastest land animal?', 'Leopard,Cheetah,Greyhound,Lion', 'Cheetah', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('Which animal is known as the "Ship of the Desert"?', 'Horse,Elephant,Camel,Donkey', 'Camel', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the only mammal capable of true flight?', 'Flying Squirrel,Bat,Eagle,Owl', 'Bat', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the largest species of shark?', 'Tiger Shark,Great White Shark,Hammerhead Shark,Whale Shark', 'Whale Shark', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is a group of lions called?', 'Herd,Pack,Pride,Swarm', 'Pride', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the primary diet of a panda?', 'Insects,Leaves,Bamboo,Fruits', 'Bamboo', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the tallest animal in the world?', 'Elephant,Moose,Horse,Giraffe', 'Giraffe', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('Which reptile has a third eye on its head?', 'Chameleon,Tuatara,Iguana,Gecko', 'Tuatara', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the fastest marine animal?', 'Dolphin,Sailfish,Marlin,Blue Whale', 'Sailfish', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('Which animal is known as the "King of the Jungle"?', 'Tiger,Lion,Jaguar,Elephant', 'Lion', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the largest species of penguin?', 'King Penguin,Adelie Penguin,Emperor Penguin,Chinstrap Penguin', 'Emperor Penguin', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the primary characteristic of amphibians?', 'Fly,Live in trees,Have feathers,Live on land and water', 'Live on land and water', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the main purpose of a kangaroo’s tail?', 'Attract mates,Attack,Balance,Defense', 'Balance', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the name of the largest land carnivore?', 'Lion,Polar Bear,Grizzly Bear,Tiger', 'Polar Bear', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('Which animal is known for its ability to regenerate lost limbs?', 'Lizard,Frog,Starfish,Octopus', 'Starfish', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the term for animals that eat both plants and meat?', 'Herbivores,Omnivores,Carnivores,Scavengers', 'Omnivores', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('Which animal is known as the "Giant of the Savanna"?', 'Rhinoceros,Hippopotamus,Elephant,Buffalo', 'Elephant', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE()),
('What is the name of the organ used by fish to breathe underwater?', 'Fins,Flippers,Gills,Scales', 'Gills', 'Zoology', @ZoologyQuizId, GETDATE(), GETDATE());


-- Insert Questions for Cinema Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('Who directed the movie "Inception"?', 'Steven Spielberg,Martin Scorsese,Christopher Nolan,James Cameron', 'Christopher Nolan', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which movie won the first-ever Academy Award for Best Picture?', 'Casablanca,The Godfather,Wings,Gone with the Wind', 'Wings', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('What is the highest-grossing film of all time?', 'Titanic,Avatar,Avengers: Endgame,The Lion King', 'Avatar', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which actress played Katniss Everdeen in "The Hunger Games"?', 'Scarlett Johansson,Anne Hathaway,Emma Watson,Jennifer Lawrence', 'Jennifer Lawrence', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('What is the name of the wizarding school in "Harry Potter"?', 'Mordor,Hogwarts,Narnia,Oz', 'Hogwarts', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Who played the Joker in "The Dark Knight"?', 'Jared Leto,Jack Nicholson,Heath Ledger,Joaquin Phoenix', 'Heath Ledger', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('What is the title of the first Star Wars movie released?', 'The Empire Strikes Back,The Phantom Menace,A New Hope,Return of the Jedi', 'A New Hope', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Who directed "Pulp Fiction"?', 'Stanley Kubrick,Quentin Tarantino,Francis Ford Coppola,Alfred Hitchcock', 'Quentin Tarantino', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which movie features the quote "Say hello to my little friend"?', 'The Godfather,Goodfellas,Heat,Scarface', 'Scarface', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('What is the name of the fictional hotel in "The Shining"?', 'The Continental,The Bates Motel,The Overlook Hotel,The Grand Budapest Hotel', 'The Overlook Hotel', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which movie features the song "Circle of Life"?', 'Aladdin,Toy Story,The Lion King,Frozen', 'The Lion King', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Who starred as the titular character in "Edward Scissorhands"?', 'Brad Pitt,Leonardo DiCaprio,Johnny Depp,Tom Cruise', 'Johnny Depp', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which animated film features the characters Woody and Buzz Lightyear?', 'Finding Nemo,The Incredibles,Shrek,Toy Story', 'Toy Story', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Who directed the movie "Psycho"?', 'Francis Ford Coppola,Orson Welles,Stanley Kubrick,Alfred Hitchcock', 'Alfred Hitchcock', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('What is the name of the ship in "Titanic"?', 'SS Lusitania,RMS Titanic,MS Britannic,SS Poseidon', 'RMS Titanic', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which movie is set on the fictional planet Pandora?', 'Dune,Avatar,Guardians of the Galaxy,Star Wars', 'Avatar', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Who played the character of Jack Sparrow in "Pirates of the Caribbean"?', 'Keira Knightley,Geoffrey Rush,Johnny Depp,Orlando Bloom', 'Johnny Depp', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which movie features the quote "Why so serious?"?', 'Saw,Pulp Fiction,Joker,The Dark Knight', 'The Dark Knight', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('What is the highest-grossing animated film of all time?', 'Frozen,Toy Story 4,Frozen II,The Lion King', 'Frozen II', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE()),
('Which movie features a robot named WALL-E?', 'Big Hero 6,WALL-E,The Iron Giant,Robots', 'WALL-E', 'Cinema', @CinemaQuizId, GETDATE(), GETDATE());

-- Insert Questions for Music Quiz

INSERT INTO Questions (Text, Options, CorrectAnswer, Category, QuizId, InsertedAt, ModifiedAt)
VALUES 
('Who is known as the King of Pop?', 'Elvis Presley,Prince,Michael Jackson,Freddie Mercury', 'Michael Jackson', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which band released the album "Abbey Road"?', 'Pink Floyd,Queen,The Beatles,The Rolling Stones', 'The Beatles', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Who is the lead singer of the band U2?', 'Bruce Springsteen,Bono,Axl Rose,Mick Jagger', 'Bono', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('What is the name of Beethoven''s only opera?', 'Carmen,Fidelio,The Magic Flute,La Traviata', 'Fidelio', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which artist is known for the song "Shape of You"?', 'Bruno Mars,Taylor Swift,Adele,Ed Sheeran', 'Ed Sheeran', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which classical composer became deaf later in life?', 'Johann Sebastian Bach,Franz Schubert,Wolfgang Amadeus Mozart,Ludwig van Beethoven', 'Ludwig van Beethoven', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which song by Queen begins with "Is this the real life?"?', 'We Will Rock You,Bohemian Rhapsody,Somebody to Love,Don''t Stop Me Now', 'Bohemian Rhapsody', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('What is the best-selling album of all time?', 'Hotel California,The Dark Side of the Moon,Back in Black,Thriller', 'Thriller', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which instrument has 88 keys?', 'Violin,Flute,Piano,Guitar', 'Piano', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which artist released the song "Rolling in the Deep"?', 'Lady Gaga,Adele,Rihanna,Beyoncé', 'Adele', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which band is known for the song "Hotel California"?', 'Lynyrd Skynyrd,Fleetwood Mac,The Eagles,The Doors', 'The Eagles', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Who composed the Four Seasons?', 'Johann Sebastian Bach,Antonio Vivaldi,Ludwig van Beethoven,Wolfgang Amadeus Mozart', 'Antonio Vivaldi', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('What is the title of Taylor Swift’s debut album?', 'Fearless,Speak Now,Red,Taylor Swift', 'Taylor Swift', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which artist is known as the Queen of Soul?', 'Tina Turner,Aretha Franklin,Whitney Houston,Diana Ross', 'Aretha Franklin', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Who performed at the 2020 Super Bowl Halftime Show alongside Shakira?', 'Lady Gaga,Katy Perry,Jennifer Lopez,Rihanna', 'Jennifer Lopez', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which song begins with "Ground Control to Major Tom"?', 'Life on Mars,Heroes,Ziggy Stardust,Space Oddity', 'Space Oddity', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Who won the Eurovision Song Contest in 2021?', 'Netta,Måneskin,Duncan Laurence,Loreen', 'Måneskin', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which instrument is Yo-Yo Ma famous for playing?', 'Flute,Violin,Cello,Piano', 'Cello', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which artist is known for the song "Purple Rain"?', 'Prince,Stevie Wonder,Michael Jackson,David Bowie', 'Prince', 'Music', @MusicQuizId, GETDATE(), GETDATE()),
('Which rapper released the album "The Marshall Mathers LP"?', 'Drake,Kanye West,Eminem,Snoop Dogg', 'Eminem', 'Music', @MusicQuizId, GETDATE(), GETDATE());

SET IDENTITY_INSERT Quizzes OFF;