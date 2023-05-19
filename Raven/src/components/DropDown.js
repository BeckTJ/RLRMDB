import React, {FC, useState, useRef} from 'react';
import {
  StyleSheet,
  Text,
  Modal,
  FlatList,
  TouchableOpacity,
  View,
} from 'react-native';

const Dropdown = props => {
  const [visible, setVisible] = useState(false);
  const DropdownButton = useRef();
  const [dropdownTop, setDropdownTop] = useState(0);
  const [selected, setSelected] = useState(undefined);

  const toggleDropdown = () => {
    visible ? setVisible(false) : openDropdown();
  };

  const openDropdown = (): void => {
    DropdownButton.current.measure((_fx, _fy, _width, height, _px, py) => {
      setDropdownTop(py + height);
    });
    setVisible(true);
  };
  const renderItem = ({item}) => (
    <TouchableOpacity style={styles.item} onPress={() => onItemPress(item)}>
      <Text>{item}</Text>
    </TouchableOpacity>
  );
  const onItemPress = (item): void => {
    setSelected(item);
    props.onSelect(item);
    setVisible(false);
  };

  const renderDropdown = () => {
    return (
      <Modal visible={visible} transparent animationType="none">
        <TouchableOpacity
          style={styles.overlay}
          onPress={() => setVisible(false)}>
          <View style={[styles.dropdown, {top: dropdownTop}]}>
            <FlatList
              data={props.data}
              renderItem={renderItem}
              keyExtractor={(item, index) => index.toString()}
            />
          </View>
        </TouchableOpacity>
      </Modal>
    );
  };

  return (
    <TouchableOpacity
      ref={DropdownButton}
      style={styles.button}
      onPress={toggleDropdown}>
      {renderDropdown()}
      <Text style={styles.buttonText}>{selected || props.label}</Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  button: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#efefef',
    height: 50,
    width: 250,
    paddingHorizontal: 10,
    zIndex: 1,
  },
  buttonText: {
    flex: 1,
    textAlign: 'center',
  },
  overlay: {
    width: 250,
  },
  dropdown: {
    position: 'absolute',
    backgroundColor: '#fff',
    width: '100%',
    shadowColor: '#000000',
    shadowRadius: 4,
    shadowOffset: {height: 4, width: 0},
    shadowOpacity: 0.5,
    alignItems: 'center',
  },
  item: {
    paddingHorizontal: 10,
    paddingVertical: 10,
    borderBottomWidth: 1,
  },
});

export default Dropdown;
