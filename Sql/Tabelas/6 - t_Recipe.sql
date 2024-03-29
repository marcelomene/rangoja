CREATE TABLE IF NOT EXISTS Recipe(
  IdRecipe INT AUTO_INCREMENT PRIMARY KEY,
  IdUser INT NOT NULL, 
  IdApplianceType INT NULL,
  Name VARCHAR(100) NOT NULL,
  Preparation VARCHAR(1000) NOT NULL,
  Image BLOB NULL,
  IdRecipeType INT NULL,
  Portion VARCHAR(30) NULL,
  PreparationTime VARCHAR(10) NULL,
  FOREIGN KEY (IdUser) REFERENCES Users(IdUser),
  FOREIGN KEY (IdRecipeType) REFERENCES Recipe_Type(IdRecipeType),
  FOREIGN KEY (IdApplianceType) REFERENCES Appliance_Type(IdApplianceType)
) ENGINE = InnoDB;