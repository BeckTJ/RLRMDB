import {NavigationHelpersContext} from '@react-navigation/native';
import React, {useEffect, useState} from 'react';
import {View, Text, StyleSheet, FlatList} from 'react-native';
import ajax from './components/ajax';

export default MaterialSelection = (props, {navigation, route}) => {
  const [material, setMaterial] = useState([]);
  const distillationOption = props.route.params.data;
  const option = props.route.params.option;

  useEffect(() => {
    async function getMaterial() {
      const materialList = await ajax.fetchMaterial();
      return setMaterial(await materialList);
    }
    getMaterial();
  }, []);

  return (
    <View style={styles.container}>
      <View style={styles.title}>
        <Text style={styles.text}>Distillation</Text>
        <Text style={styles.text}>{distillationOption.name}</Text>
      </View>
      <FlatList
        numColumns={2}
        columnWrapperStyle={{justifyContent: 'space-evenly'}}
        ItemSeparatorComponent={<View style={{margin: '5%'}} />}
        style={styles.buttons}
        data={material}
        renderItem={({item}) => (
          <LargeButton
            key={item.materialNumber}
            name={item.materialName}
            currentOption={'Material Selection'}
            currentRoute={distillationOption}
          />
        )}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    margin: '5%',
  },
  title: {
    alignItems: 'center',
    justifyContent: 'center',
  },
  text: {
    fontSize: 68,
  },
  buttons: {
    marginTop: '10%',
    flex: 1,
    flexDirection: 'column',
  },
});
