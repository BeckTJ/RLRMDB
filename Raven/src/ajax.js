const apiHost = 'http://10.0.2.2:5263';

export default {
  async fetchMaterial() {
    try {
      const response = await fetch(apiHost + '/Material');
      const responseJson = await response.json();
      return responseJson;
    } catch (error) {
      console.error(error);
    }
  },

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
};
