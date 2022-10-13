const apiHost = 'https://localhost:7218/RavenAPI';

export default {
  async fetchMaterial() {
    try {
      let response = await fetch(apiHost + '/Material');
      let responseJson = await response.json();
      return responseJson;
    } catch (error) {
      console.error(error);
    }
  },
};
