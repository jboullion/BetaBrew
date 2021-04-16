/// <reference types="cypress" />
context('SideMenu', () => {
  beforeEach(() => {
    cy.visit('http://localhost:8080/')
  })

  it('is the inventory page', () => {
    cy.get('#inventoryTitle').contains('Inventory');
  });

  it('has buttons to add inventory and receive shipment', () => {
    cy.get('#addNewBtn > .beta-button').contains('Add New Item');
    cy.get('#receiveShipmentBtn > .beta-button').contains('Receive Shipment');
  });

  it('has add new item modal on click', () => {
    cy.get('#addNewBtn').click();
    cy.get('#modalTitle').contains('New Product');
    cy.get('[aria-label="Close product modal"] > .beta-button').click();
  });

  it('adding new product and closing modal before save does not add new product', () =>{
    cy.get('#addNewBtn').click();
    cy.get('#isTaxable').click();
    cy.get('#productName').clear();
    cy.get('#productName').type('Test Product', { delay: 60 });
    cy.get('#productDescription').type('Test Product description', { delay: 60 });
    cy.get('#productPrice').clear();
    cy.get('#productPrice').type('120', { delay: 60 });
    cy.get('[aria-label="Close product modal"] > .beta-button').click();
  });
  
})