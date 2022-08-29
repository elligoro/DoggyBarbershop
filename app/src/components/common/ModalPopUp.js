//https://www.npmjs.com/package/react-modal

import React, { Component, useState, useEffect } from 'react';
import ReactDOM from 'react-dom';
import Modal from 'react-modal';

const customStyles = {
  content: {
    top: '50%',
    left: '50%',
    right: 'auto',
    bottom: 'auto',
    marginRight: '-50%',
    transform: 'translate(-50%, -50%)',
    maxWidth: '450px',
    margin: 'auto',
    maxHeight: '450px'
  },
};

/*
class ModalPopUp extends Component{

  constructor({ component: ModalContent, ...props }){
    super();

    Modal.setAppElement(props.containerId);
    this.state = {isOpen: false};
  }

  openModel(){
    this.useState(...this.state, {isOpen: true});
  }

  closeModel(){
    this.useState(...this.state, {isOpen: false});
  }

// Make sure to bind modal to your appElement (https://reactcommunity.org/react-modal/accessibility/)
 
/*
 export default function ModlPopUp() {
  let subtitle;
  const [modalIsOpen, setIsOpen] = React.useState(false);

  function openModal() {
    setIsOpen(true);
  }

  function afterOpenModal() {
    // references are now sync'd and can be accessed.
    subtitle.style.color = '#f00';
  }

  function closeModal() {
    setIsOpen(false);
  }
*/

// https://bobbyhadz.com/blog/react-update-state-when-props-change

function ModalPopUp({component:ModalContent, ...props}){

  const [modalIsOpen, setIsOpen] = useState(false);
  const { isOpenModal,containerId,onSubmit,onClose, order } = props;

  Modal.setAppElement(containerId);
  useEffect(()=>{
    setIsOpen(props.isOpenModal);

  },[isOpenModal]);

  function openModal() {
    setIsOpen(true);
  }

  function closeModal() {
    setIsOpen(false);
  }

  return (
    <div>
      <Modal
        isOpen={isOpenModal}
        onRequestClose={closeModal}
        contentLabel="Example Modal"
      >
        <ModalContent onSubmit={onSubmit} onClose={onClose} order={order}/>
      </Modal>
    </div>
  );
}

export default ModalPopUp;