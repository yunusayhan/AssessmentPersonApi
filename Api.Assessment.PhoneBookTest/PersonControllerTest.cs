using Assessment.Phonebook.Api.Controllers;
using Assessment.Phonebook.Domain.Entity;
using Assessment.Phonebook.Services.DTO;
using Assessment.Phonebook.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Assessment.PhoneBookTest
{

    public class PersonControllerTest
    {
        private readonly Mock<IPersonServices> _mockRepo;
        private readonly PersonController _controller;
        private List<GetPersonDTO> persons;
        private List<PersonDto> personsPost;
        private List<PersonDetailDTO> personsDetailPost;
        public PersonControllerTest()
        {
            _mockRepo = new Mock<IPersonServices>();
            _controller = new PersonController(_mockRepo.Object);

            personsPost = new List<PersonDto>() {new PersonDto {
            Name="Yunus",
            Surname="Ayhan",
            Company="Test Şirket"
            },

              new PersonDto {
            Name="Emre",
            Surname="Ayhan",
            Company="Test Şirket2"
            } };

            personsDetailPost = new List<PersonDetailDTO>() {new PersonDetailDTO {
           Location="Pendik",
           MailAddress="ynsayn@gmail.com",
           PersonId=1,
           PhoneNumber="556565665"
            },
                {new PersonDetailDTO {
           Location="Pendik",
           MailAddress="ynsayn@gmail.com",
           PersonId=1,
           PhoneNumber="556565665"
            } } };

        }


        [Theory]
        [InlineData(1)]
        public async void GetPerson_IdInValid_ReturnOkResult(int personId)
        {
            var person = new GetPersonDTO();
            ItemDto itemDtoRequest = new ItemDto();
            itemDtoRequest.Id = personId;
            _mockRepo.Setup(x => x.GetPerson(It.IsAny<ItemDto>())).Returns(() => Task.FromResult(person as GetPersonDTO));
            var result = await _controller.GetPerson(personId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(x => x.GetPerson(It.IsAny<ItemDto>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        public async void GetPersonDetail_IdInValid_ReturnOkResult(int personId)
        {
            var person = new GetPersonDTO();
            ItemDto itemDtoRequest = new ItemDto();
            itemDtoRequest.Id = personId;
            _mockRepo.Setup(x => x.GetPersonDetail(It.IsAny<ItemDto>())).Returns(() => Task.FromResult(person as GetPersonDTO));
            var result = await _controller.GetPersonDetail(personId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            _mockRepo.Verify(x => x.GetPersonDetail(It.IsAny<ItemDto>()), Times.Once);
        }

        [Fact]
        public async void PostPerson_ActionExecute_ReturnCreatedAction()
        {
            var person = personsPost.First();
            var returnModel = new ResultModel();
            _mockRepo.Setup(x => x.CreatePerson(person)).Returns(() => Task.FromResult(returnModel as ResultModel));
            var result = await _controller.CreatePerson(person);
            var created = Assert.IsType<CreatedResult>(result);
            _mockRepo.Verify(x => x.CreatePerson(person), Times.Once);

        }

        [Fact]
        public async void PostPersonDetail_ActionExecute_ReturnCreatedAction()
        {
            var person = personsDetailPost.First();
            var returnModel = new ResultModel();
            _mockRepo.Setup(x => x.CreatePersonDetail(person)).Returns(() => Task.FromResult(returnModel as ResultModel));
            var result = await _controller.CreatePersonDetail(person);
            var created = Assert.IsType<CreatedResult>(result);
            _mockRepo.Verify(x => x.CreatePersonDetail(person), Times.Once);

        }

        [Theory]
        [InlineData(1)]
        public async void DeletePerson_IdInValid_ReturnNotContent(int id)
        {
            ItemDto itemDtoRequest = new ItemDto();
            itemDtoRequest.Id = id;
            var returnModel = new ResultModel();

            _mockRepo.Setup(x => x.DeletePerson(It.IsAny<ItemDto>())).Returns(() => Task.FromResult(returnModel as ResultModel));
            var result = await _controller.DeletePerson(2);
            var created = Assert.IsType<NoContentResult>(result);
            _mockRepo.Verify(x => x.DeletePerson(It.IsAny<ItemDto>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        public async void DeletePersonDetail_IdInValid_ReturnNotContent(int id)
        {
            ItemDto itemDtoRequest = new ItemDto();
            itemDtoRequest.Id = id;
            var returnModel = new ResultModel();

            _mockRepo.Setup(x => x.DeletePersonDetail(It.IsAny<ItemDto>())).Returns(() => Task.FromResult(returnModel as ResultModel));
            var result = await _controller.DeletePersonDetail(2);
            var created = Assert.IsType<NoContentResult>(result);
            _mockRepo.Verify(x => x.DeletePersonDetail(It.IsAny<ItemDto>()), Times.Once);
        }
    }
}
