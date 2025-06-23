import styled from 'styled-components';

export const Container = styled.div`
  position: relative;
  margin-top: 20px;

  @media (min-width: 960px) {
    margin-top: 81px;
  }

  .scroll-box__wrapper {
    display: flex;
    overflow-x: scroll;
    overflow-y: hidden;
    padding: 4px 20px;
    scroll-behavior: smooth;
    -ms-overflow-style: none;
    scrollbar-width: none;
  }

  .scroll-box__wrapper::-webkit-scrollbar {
    display: none;
  }

  main {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    padding: 15px 25px;
    border: 1px solid #ccc;
    border-radius: 10px;
    min-width: 120px;
    background-color: #fff;
    box-sizing: border-box;

    & + main {
      margin-left: 10px;

      @media (min-width: 960px) {
        margin-left: 8px;
      }

      @media (min-width: 1140px) {
        margin-left: 15px;
      }
    }

    :last-child {
      padding-right: 20px;
    }

    span {
      font-size: 1rem;
      color: #717171;
      font-weight: 300;
      text-align: center;
      user-select: none;
    }
  }
`;

export const ArrowButton = styled.button<{ direction: 'left' | 'right' }>`
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  ${({ direction }) => (direction === 'left' ? 'left: 0' : 'right: 0')};
  background: transparent;
  border: none;
  font-size: 1.5rem;
  padding: 10px;
  cursor: pointer;
  z-index: 1;
  border-radius: 50%;
  transition: background 0.2s;

  &:hover {
      box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
  }
`;
