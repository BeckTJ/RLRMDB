import {Alert} from 'react-native';

const apiHost = 'http://localhost:5263';

export default {
  async fetchRunLog(materialNumber) {
    try {
      const response = await fetch(
        apiHost + '/RunLog/(HourlyRead)?materialNumber=' + materialNumber,
      );
      const responseJson = await response.json();
      return responseJson;
    } catch (error) {
      console.error(error);
    }
  },
  async fetchRawMaterial(materialNumber, vendor) {
    try {
      const response = await fetch(
        apiHost +
          '/Product/(RawMaterial)?materialNumber=' +
          materialNumber +
          '&vendor=' +
          vendor,
      );
      const responseJson = await response.json();
      return responseJson;
    } catch (error) {
      console.error(error);
    }
  },
  async fetchRawMaterialId(rawMaterial) {
    try {
      const response = await fetch(
        apiHost + '/Product/(BatchNumber)?batchNumber' + rawMaterial,
      );
      const responseJson = await response.json();
      return responseJson;
    } catch (error) {
      console.error(error);
    }
  },
  async fetchPreStart(materialNumber) {
    try {
      const response = await fetch(
        apiHost + '/RunLog/(PreStart)?materialNumber=' + materialNumber,
      );
      const responseJson = await response.json();
      return responseJson;
    } catch (error) {
      console.error(error);
    }
  },

  async fetchProduct(materialNumber) {
    try {
      const response = await fetch(
        apiHost + '/Product/(Selection)?materialNumber=' + materialNumber,
      );
      const responseJson = await response.json();
      return responseJson;
    } catch (error) {
      console.error(error);
    }
  },
  async postProduct(product) {
    await fetch(apiHost + '/Product/UpdateProductLot', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        productLotNumber: product.lotNumber,
        materialNumber: product.materialNumber,
        productBatchNumber: product.batchNumber,
        processOrder: product.processOrder,
        receiverId: product.receiver,
        sampleSubmitNumber: product.sampleNumber,
        startDate: product.startDate,
      }),
    }).then(response => {
      response.json();
    });
  },
};
