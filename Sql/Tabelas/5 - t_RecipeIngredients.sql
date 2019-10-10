CREATE TABLE IF NOT EXISTS Recipe_Ingredients(
  IdRecipe INT NOT NULL,
  IdIngredient INT NOT NULL,
  IdUnitType INT NULL,
  Amount VARCHAR(30) NULL,
  FOREIGN KEY (IdRecipe) REFERENCES Recipe(IdRecipe),
  FOREIGN KEY (IdIngredient) REFERENCES Ingredients(IdIngredient),
  FOREIGN KEY (IdUnitType) REFERENCES Units(IdUnitType)
) ENGINE = InnoDB;