using Bunit;
using CleanDotnetBlazor.Server.Infrastructure;
using CleanDotnetBlazor.Shared;
using Client.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;
using TestContext = Bunit.TestContext;

namespace CleanDotnetBlazor.AcceptanceTests.Steps;

[Binding]
public sealed class CustomerStepDefinitions
{
    private CustomerDto _customer;
    private IRenderedComponent<Customer> _component;
    private bool _onSavedCalled;

    public CustomerStepDefinitions()
    {
    }

    [Given("a new customer")]
    public void GivenANewCustomer()
    {
        _customer = new CustomerDto
        {
            FirstName = "Mehdi",
            LastName = "Rajaei",
            PhoneNumber = "+989152041565",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today)
        };
    }

    [When("the new customer is valid")]
    public async Task TheNewCustomerIsValid()
    {
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Created
        };

        var mockHandler = new MockHttpMessageHandler(mockResponse);
        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(Constants.BaseUrl)
        };

        var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(httpClient);

        _component = ctx.RenderComponent<Customer>(parameters => parameters
            .Add(p => p.OnSaved, EventCallback.Factory.Create(this, () => _onSavedCalled = true))
        );

        await _component.Instance.Open(0);

        _component.Find("#first-name").Change(_customer.FirstName);
        _component.Find("#last-name").Change(_customer.LastName);
        _component.Find("#date-of-birth").Change(_customer.DateOfBirth.ToString("yyyy-MM-dd"));
        _component.Find("#phone-number").Change(_customer.PhoneNumber);

        await _component.Find("form").SubmitAsync();
    }

    [Then("the customer is created successfully")]
    public void TheCustomerIsCreatedSuccessfully()
    {
        Assert.AreEqual("none", _component.Instance.ModalDisplay);
        Assert.True(_onSavedCalled, "OnSaved event should be triggered upon successful creation.");
    }

    [When("the new customer is not valid")]
    public async Task TheCustomerIsNotValid()
    {
        _customer.PhoneNumber = "123";
        var errors = new Dictionary<string, string[]>() { 
            { "PhoneNumber", new string[] { "'Phone Number' is not valid." } } 
        };

        var mockResponse = CustomExceptionHandler.GetBadRequestResponseMessage(errors);
        var mockHandler = new MockHttpMessageHandler(mockResponse);
        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(Constants.BaseUrl)
        };

        var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(httpClient);

        _component = ctx.RenderComponent<Customer>(parameters => parameters
            .Add(p => p.OnSaved, EventCallback.Factory.Create(this, () => _onSavedCalled = true))
        );

        await _component.Instance.Open(0);

        _component.Find("#first-name").Change(_customer.FirstName);
        _component.Find("#last-name").Change(_customer.LastName);
        _component.Find("#date-of-birth").Change(_customer.DateOfBirth.ToString("yyyy-MM-dd"));
        _component.Find("#phone-number").Change(_customer.PhoneNumber);

        await _component.Find("form").SubmitAsync();
    }

    [Then("an error is displayed")]
    public void AnErrorIsDisplayed()
    {
        Assert.AreEqual("block", _component.Instance.ModalDisplay);
        Assert.False(_onSavedCalled, "OnSaved event should not be triggered due to creation errors.");
    }

    [Given("an existing customer")]
    public void GivenAnExistingCustomer()
    {
        _customer = new CustomerDto
        {
            Id = 5,
            FirstName = "Mehdi",
            LastName = "Rajaei",
            PhoneNumber = "+989152041565",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today).AddMonths(-1)
        };
    }

    [When("the changes on the customer are valid")]
    public async Task ChangeOnAnExistingCustomerAreValid()
    {
        var jsonString = JsonSerializer.Serialize(_customer);

        var mockHandler = new MockHttpMessageHandler(request =>
        {
            if (request.Method == HttpMethod.Get)
            {
                var jsonString = JsonSerializer.Serialize(_customer);
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
                };
            }
            else
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Created
                };
            }
        });

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(Constants.BaseUrl)
        };

        var ctx = new TestContext();
        ctx.Services.AddSingleton<HttpClient>(httpClient);

        _component = ctx.RenderComponent<Customer>(parameters => parameters
            .Add(p => p.OnSaved, EventCallback.Factory.Create(this, () => _onSavedCalled = true))
        );

        await _component.Instance.Open(_customer.Id);

        _component.Find("#first-name").Change(_customer.FirstName);
        _component.Find("#last-name").Change(_customer.LastName);
        _component.Find("#date-of-birth").Change(_customer.DateOfBirth.ToString("yyyy-MM-dd"));
        _component.Find("#phone-number").Change(_customer.PhoneNumber);

        await _component.Find("form").SubmitAsync();
    }

    [Then("the customer is updated successfully")]
    public void TheCustomerIsUpdatedSuccessfully()
    {
        Assert.AreEqual("none", _component.Instance.ModalDisplay);
        Assert.True(_onSavedCalled, "OnSaved event should be triggered upon successful creation.");
    }
}