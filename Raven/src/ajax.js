const apiHost = 'http://localhost:5263';

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
};
